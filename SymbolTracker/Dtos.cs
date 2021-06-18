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
        public string Datetime { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Close { get; set; }
        public string Volume { get; set; }
    }
}