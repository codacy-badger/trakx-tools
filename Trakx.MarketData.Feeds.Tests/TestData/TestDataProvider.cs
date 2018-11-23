﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Trakx.MarketData.Feeds.Tests.TestData
{
    internal class TestDataProvider
    {
        public const string Testdata = "TestData";
    }

    internal class CryptoCompare
    {
        public Lazy<string> CoinListAsString;

        public CryptoCompare()
        {
            var coinListJsonFile = Path.Combine(Environment.CurrentDirectory, TestDataProvider.Testdata, "cryptocompare-coinlist.json");
            CoinListAsString = new Lazy<string>(() => File.ReadAllText(coinListJsonFile), LazyThreadSafetyMode.PublicationOnly);

        }
    }
}