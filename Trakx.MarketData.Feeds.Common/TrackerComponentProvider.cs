﻿using System;
using System.Collections.Generic;
using System.Text;

using Trakx.MarketData.Feeds.Common.Models.CryptoCompare;

namespace Trakx.MarketData.Feeds.Common
{
    public interface ITrackerComponentProvider
    {
        /// <summary>
        /// Details of the current best coins by market cap.
        /// </summary>
        /// <param name="coinCount">Number of coins expected in the list (ex: pass 10 to get the top 10).</param>
        /// <returns>The list of top <see cref="coinCount"/> coins by usd market cap.</returns>
        IReadOnlyDictionary<ICoin, decimal> GetTopXMarketCapCoins(uint coinCount);
    }
}