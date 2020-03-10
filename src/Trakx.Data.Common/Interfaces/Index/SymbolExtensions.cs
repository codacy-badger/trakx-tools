﻿using System.Text.RegularExpressions;

namespace Trakx.Data.Common.Interfaces.Index
{
    public static class SymbolExtensions
    {
        public static readonly Regex IndexSymbolRegex = new Regex(
            @"^(?<longShort>l|s)(?<leverage>[0-9]+)(?<sectorTicker>[a-zA-z]{3})$");
        public static readonly Regex CompositionSymbolRegex = new Regex(
            @"^(?<longShort>l|s)(?<leverage>[0-9]+)(?<sectorTicker>[a-zA-z]{3})(?<dateTicker>[0-9][0-9]0[1-9]|^(11)|^(12))$");

        public static bool IsIndexSymbol(this string candidateSymbol)
        {
            return IndexSymbolRegex.IsMatch(candidateSymbol);
        }

        public static bool IsCompositionSymbol(this string candidateSymbol)
        {
            return CompositionSymbolRegex.IsMatch(candidateSymbol);
        }
    }
}