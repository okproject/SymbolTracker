using Newtonsoft.Json.Linq;

namespace SymbolTracker.Services
{
    public interface IStockServiceClient
    {
        JObject GetTimeSeries();
    }
}