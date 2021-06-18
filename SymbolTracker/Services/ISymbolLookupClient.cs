using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SymbolTracker.Services
{
    public interface ISymbolLookupClient
    {
        Task<List<StockSymbol>> GetTimeSeries();
    }
}