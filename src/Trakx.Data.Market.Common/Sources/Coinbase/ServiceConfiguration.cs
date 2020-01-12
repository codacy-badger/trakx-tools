﻿using Microsoft.Extensions.DependencyInjection;

namespace Trakx.Data.Market.Common.Sources.Coinbase
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddCoinbaseClient(this IServiceCollection services)
        {
            services.AddSingleton<ICoinbaseClient, CoinbaseClient>();

            return services;
        }
    }
}