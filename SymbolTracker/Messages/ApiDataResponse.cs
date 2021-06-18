using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SymbolTracker.Messages
{
    public class ApiDataResponse
    {
        public ApiDataResponse(IEnumerable<StockSymbol> data)
        {
            Data = data ?? new List<StockSymbol>();
        }

        public IEnumerable<StockSymbol> Data { get; private set; }
    }
}