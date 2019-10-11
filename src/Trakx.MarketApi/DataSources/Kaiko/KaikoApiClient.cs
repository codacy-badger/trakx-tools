﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Trakx.MarketApi.DataSources.Kaiko
{
    public class KaikoApiClient : IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _apiKey;
        private readonly string _marketDataEndpoint;
        private readonly string _referenceDataEndpoint;
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _httpReferenceDataClient;
        private HttpClient _httpMarketDataClient;

        public KaikoApiClient(/*IHttpClientFactory httpClientFactory*/ILogger logger)
        {
            _logger = logger;
            //_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            //todo: this is probably one of the worst ways to do HttpRequests
            //https://josefottosson.se/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/

            _apiKey = "75d63ffdbc021f188e63fe15f12a881a";
            _marketDataEndpoint = "https://eu.market-api.kaiko.io/v1/";
            _referenceDataEndpoint = "https://reference-data-api.kaiko.io/v1/";

            _httpReferenceDataClient = new HttpClient { BaseAddress = new Uri(_referenceDataEndpoint) };
            _httpReferenceDataClient.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
            _httpMarketDataClient = new HttpClient { BaseAddress = new Uri(_marketDataEndpoint) };
            _httpMarketDataClient.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
        }

        public async Task<AssetsResponse> GetAssets()
        {
            var response = await _httpReferenceDataClient.GetAsync("assets").ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<AssetsResponse>(content);
        }

        public async Task<InstrumentsResponse> GetInstruments()
        {
            var response = await _httpReferenceDataClient.GetAsync("instruments").ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<InstrumentsResponse>(content);
        }

        public async Task<ExchangesResponse> GetExchanges()
        {
            var response = await _httpReferenceDataClient.GetAsync("exchanges").ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ExchangesResponse>(content);
        }

        public async Task<AggregatedPrice.Response> GetAggregatedPrice(AggregatedPrice.Query query)
        {
            //todo:  build the URL from a HttpQuery, not manually like that
            var startTimeIso8601 = query.StartTime.DateTime.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);
            var path = _marketDataEndpoint + $"data/{query.Commodity}.{query.DataVersion}/" +
                $"spot_direct_exchange_rate/{query.BaseAsset}/{query.QuoteAsset}?"
                                    + $"start_time={UrlEncoder.Default.Encode(startTimeIso8601)}"
                                    + $"&interval={query.Interval}"
                                    + $"&page_size={query.PageSize}"
                                    + $"&exchanges={string.Join(",", query.Exchanges)}"
                                    + $"&sources={query.Sources.ToString().ToLower()}";
            try
            {
                var response = await _httpMarketDataClient.GetAsync(path).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var converted = JsonConvert.DeserializeObject<AggregatedPrice.Response>(content);
                return converted;
            }
            catch (Exception exception)
            {
                _logger.LogError("Failed to retrieve data for {0}", query.BaseAsset, exception);
                return null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _httpReferenceDataClient?.Dispose();
            _httpMarketDataClient?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
