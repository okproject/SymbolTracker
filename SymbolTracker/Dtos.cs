using System;
using System.Collections.Generic;

namespace SymbolTracker
{
    public class TwelveDataApiResponse
    {
        public List<StockSymbol> Symbols { get; set; }
    }

    public class StockSymbol
    {
        public string Symbol { get; set; }
        public Meta Meta { get; set; }  
        public List<Value> Values { get; set; }
        public string Status { get; set; }
    }

    public class Meta
    {
        public string Symbol { get; set; }
        public string Interval { get; set; }
        public string Currency { get; set; }
        public string ExchangeTimezone { get; set; }
        public string Exchange { get; set; }
        public string Type { get; set; }
        public string CurrencyBase { get; set; }
        public string CurrencyQuote { get; set; }
    }

    public class Value
    {
        public DateTime Datetime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public long Volume { get; set; }
    }
}