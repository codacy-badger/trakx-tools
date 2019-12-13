﻿using System;
using System.Text.Json.Serialization;

namespace Trakx.Data.Market.Common.Sources.Messari.DTOs
{
    public partial class SalesRound
    {
        [JsonPropertyName("roundName")]
        public string RoundName { get; set; }

        [JsonPropertyName("startDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTimeOffset EndDate { get; set; }

        [JsonPropertyName("pricePerUnit")]
        public double? PricePerUnit { get; set; }

        [JsonIgnore]
        //[JsonPropertyName("unit")]
        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public Unit Unit { get; set; }

        [JsonPropertyName("amountCollected")]
        public double? AmountCollected { get; set; }

        [JsonPropertyName("restriction")]
        public object Restriction { get; set; }
    }
}