using Newtonsoft.Json.Linq;

namespace SymbolTracker.Services
{
    public interface ISymbolLookupClient
    {
        JObject GetTimeSeries();
    }
}