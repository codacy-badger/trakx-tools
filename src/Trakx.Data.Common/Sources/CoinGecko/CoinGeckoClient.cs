﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Trakx.Data.Common.Pricing;
using Trakx.Data.Common.Utils;

namespace Trakx.Data.Common.Sources.CoinGecko
{
    public class CoinGeckoClient : ICoinGeckoClient
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CoinGeckoClient> _logger;
        private readonly ICoinsClient _coinsClient;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly ISimpleClient _simpleClient;
        private Dictionary<string, string> _idsByName;
        private Dictionary<string, string> _symbolsByNames;
        private Dictionary<string, string> _idsBySymbol;
        private Dictionary<string, string> _idsBySymbolName;

        public Dictionary<string, string> IdsBySymbolName => _idsBySymbolName ??= CoinList
            .ToDictionary(c => GetSymbolNameKey(c.Symbol, c.Name), c => c.Id);

        private readonly Dictionary<string, CoinFullDataById> _coinFullDataByIds;
        public Dictionary<string, CoinFullDataById> CoinFullDataByIds => _coinFullDataByIds;

        private static readonly Regex etherscanTokenAddress = new Regex(@"https://etherscan.io/token/(?<address>0x\w{40})");

        public CoinGeckoClient(ClientFactory factory, IMemoryCache memoryCache, ILogger<CoinGeckoClient> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, c => TimeSpan.FromSeconds(c*c));
            _coinsClient = factory.CreateCoinsClient();
            _simpleClient = factory.CreateSimpleClient();
            _coinFullDataByIds = new Dictionary<string, CoinFullDataById>();
        }

        /// <inheritdoc />
        public async Task<decimal?> GetLatestPrice(string symbol, string quoteCurrency = Constants.DefaultQuoteCurrency)
        {
            var id = await GetCoinGeckoIdFromSymbol(symbol);
            if (id == default) return 0;

            var quoteCurrencyId = quoteCurrency.ToLower() == Constants.Usd 
                ? Constants.Usd 
                : await GetCoinGeckoIdFromSymbol(quoteCurrency);
            if (quoteCurrencyId == default) return 0;

            var tickerDetails = await _retryPolicy.ExecuteAsync(
                () => _simpleClient.GetSimplePrice(new []{id, quoteCurrencyId }, new []{ Constants.Usd }))
                .ConfigureAwait(false);
            var price = tickerDetails[id][Constants.Usd];
            var conversionToQuoteCurrency = tickerDetails[quoteCurrencyId][Constants.Usd];
            return (decimal?)(price / conversionToQuoteCurrency) ?? 0m;
        }

        private async Task<string?> GetCoinGeckoIdFromSymbol(string symbol)
        {
            var coinList = await GetCoinList();

            var id = coinList.FirstOrDefault(c =>
                c.Symbol.Equals(symbol, StringComparison.InvariantCultureIgnoreCase))?.Id;
            return id;
        }

        /// <inheritdoc />
        public async Task<decimal?> GetPriceAsOf(string symbol, DateTime asOf, string quoteCurrency = Constants.DefaultQuoteCurrency)
        {
            var id = await GetCoinGeckoIdFromSymbol(symbol);
            if (id == default) return 0;

            var quoteId = await GetCoinGeckoIdFromSymbol(quoteCurrency);

            return await GetPriceAsOfFromId(id, asOf, quoteId)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<decimal?> GetPriceAsOfFromId(string id, DateTime asOf, string quoteCurrencyId = "usd-coin")
        {
            try
            {
                var date = asOf.ToString("dd-MM-yyyy");

                var conversion = 1m;
                if (quoteCurrencyId != default)
                {
                    var quoteResponse = await _memoryCache.GetOrCreateAsync($"{date}|{quoteCurrencyId}",
                        async entry => await _retryPolicy.ExecuteAsync(() =>
                        _coinsClient.GetHistoryByCoinId(quoteCurrencyId, date, false.ToString())));
                    conversion = (decimal?)quoteResponse.MarketData.CurrentPrice[Constants.Usd] ?? 1m;
                }

                var historicalPrice = await _retryPolicy.ExecuteAsync(() =>
                        _coinsClient.GetHistoryByCoinId(id, date, false.ToString()));

                return (decimal?)historicalPrice.MarketData.CurrentPrice[Constants.Usd] / conversion;
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Failed to retrieve price for {0} as of {1:yyyyMMdd}", id, asOf);
                return null;
            }
        }

        public bool TryRetrieveSymbol(string coinName, out string? symbol)
        {
            _symbolsByNames ??= CoinList.ToDictionary(c => c.Name, c => c.Symbol);
            var bestMatch = coinName.FindBestLevenshteinMatch(_symbolsByNames.Keys);
            symbol = bestMatch != null ? _symbolsByNames[bestMatch] : null;

            return bestMatch != null;
        }

        private string GetSymbolNameKey(string symbol, string name)
        {
            return $"{symbol.ToLower()}|{name.ToLower()}";
        }

        public string RetrieveCoinGeckoId(string symbol, string name)
        {
            var symbolNameKey = GetSymbolNameKey(symbol, name);
            if (IdsBySymbolName.TryGetValue(symbolNameKey, out var coinGeckoId))
                return coinGeckoId;

            var bestMatch = symbolNameKey.FindBestLevenshteinMatch(IdsBySymbolName.Keys);
            coinGeckoId = IdsBySymbolName[bestMatch];
            IdsBySymbolName.Add(symbolNameKey, coinGeckoId);
            return coinGeckoId;
        }

        public CoinFullDataById RetrieveCoinFullData(string symbol, string name)
        {
            var coinId = RetrieveCoinGeckoId(symbol, name);
            if (CoinFullDataByIds.TryGetValue(coinId, out var data)) return data;

            try
            {
                data = _coinsClient.GetAllCoinDataWithId(coinId, "false",
                false, false, false, false, false)
                    .GetAwaiter().GetResult();

                CoinFullDataByIds[coinId] = data;
                return data;
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "Failed to retrieve coin data for {0}|{1}", symbol, name);
                return default;
            }
        }

        public bool TryRetrieveContractDetailsFromCoinName(string coinName, 
            out string? coinGeckoId, out string? symbol, out string? etherscanLink)
        {
            coinGeckoId = string.Empty;
            symbol = string.Empty;
            etherscanLink = string.Empty;

            _idsByName ??= CoinList.ToDictionary(c => c.Name, c => c.Id);
            var bestMatch = coinName.FindBestLevenshteinMatch(_idsByName.Keys);
            if (bestMatch == null) return false;
            coinGeckoId = _idsByName[bestMatch];

            return TryGetCoinDataWithId(ref coinGeckoId, ref symbol, ref etherscanLink);
        }

        public void RetrieveContractDetailsFromCoinSymbolName(string searchSymbol, string searchName, 
            out string? coinGeckoId, out string? symbol, out string? etherscanLink)
        {
            var data = RetrieveCoinFullData(searchSymbol, searchName);
            coinGeckoId = data?.Id;
            symbol = data?.Symbol;
            etherscanLink = data?.Links?.BlockchainSite?.FirstOrDefault(b => b != null && etherscanTokenAddress.IsMatch(b));
        }



        private bool TryGetCoinDataWithId(ref string coinGeckoId, ref string symbol, ref string etherscanLink)
        {
            try
            {
                var details = CoinFullDataByIds[coinGeckoId];
                symbol = details.Symbol;
                etherscanLink = details.Links.BlockchainSite.FirstOrDefault(b => etherscanTokenAddress.IsMatch(b));
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Failed to retrieve contract details for {0}", coinGeckoId);
                return false;
            }
        }

        public async Task<IReadOnlyList<CoinList>> GetCoinList()
        {
            var coinList = await _memoryCache.GetOrCreateAsync("CoinGecko.CoinList", async entry =>
                await _retryPolicy.ExecuteAsync(() => _coinsClient.GetCoinList()).ConfigureAwait(false));
            return coinList;
        }

        public IReadOnlyList<CoinList> CoinList  => _memoryCache.GetOrCreate("CoinGecko.CoinList", 
            entry => _retryPolicy.ExecuteAsync(() => _coinsClient.GetCoinList())
                .ConfigureAwait(false).GetAwaiter().GetResult());
    }
}