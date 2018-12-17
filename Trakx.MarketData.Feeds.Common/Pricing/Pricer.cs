﻿using System;
using System.Collections.Generic;
using System.Linq;

using CryptoCompare;

using Trakx.MarketData.Feeds.Common.Trackers;

namespace Trakx.MarketData.Feeds.Common.Pricing
{
    public class Pricer : IPricer
    {
        private const decimal TrackersToUnderlyingVolumeRatio = 0.3m;
        private const decimal MarketCapToVolumeRatio = 10m;

        private readonly ITrackerFactory _trackerFactory;
        
        public Pricer(ITrackerFactory trackerFactory)
        {
            _trackerFactory = trackerFactory;
        }
        /// <inheritdoc />
        public IDictionary<string, decimal> CalculatePricesByCurrencies(
            string ticker,
            IList<IReadOnlyDictionary<string, decimal>> componentPriceByCurrency)
        {
            var tracker = _trackerFactory.FromTicker(ticker);
            var currencies = componentPriceByCurrency.SelectMany(p => p.Keys).Distinct();
            var result = currencies.ToDictionary(
                c => c,
                c => componentPriceByCurrency.Average(p => p[c]) * tracker.Leverage);

            return result;
        }

        /// <inheritdoc />
        public IDictionary<string, decimal> CalculatePricesByCurrencies(
            string ticker,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, decimal>> priceByCurrencyByTicker)
        {
            var tracker = _trackerFactory.FromTicker(ticker);
            var currencies = priceByCurrencyByTicker.Values.SelectMany(v => v.Keys).Distinct();
            var result = currencies.ToDictionary(
                c => c,
                c => priceByCurrencyByTicker.Average(p => p.Value[c]) * tracker.Leverage);

            return result;
        }

        public decimal? LeveragedAverage(int leverage, IList<CoinFullAggregatedData> componentData, Func<CoinFullAggregatedData, decimal?> selector)
        {
            return componentData.Select(selector).Average() * leverage;
        }
        public double? LeveragedAverage(int leverage, IList<CoinFullAggregatedData> componentData, Func<CoinFullAggregatedData, double?> selector)
        {
            return componentData.Select(selector).Average() * leverage;
        }

        /// <inheritdoc />
        public decimal CalculateVolumeFromUnderlyingVolumeTo(List<decimal> componentVolumes)
        {
            return TrackersToUnderlyingVolumeRatio * componentVolumes.Average();
        }

        /// <inheritdoc />
        public decimal? CalculateVolumeFromUnderlyingVolumeTo(List<decimal?> componentVolumes)
        {
            return TrackersToUnderlyingVolumeRatio * componentVolumes.Average();
        }

        /// <inheritdoc />
        public double? CalculateVolumeFromUnderlyingVolumeTo(List<double?> componentVolumes)
        {
            return (double)TrackersToUnderlyingVolumeRatio * componentVolumes.Average();
        }

        /// <inheritdoc />
        public decimal? CalculateMarketCapFrom24hVolumeTo(decimal? volume24HTo)
        {
            return MarketCapToVolumeRatio * volume24HTo;
        }
    }
}