using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace SymbolTracker.Services
{
    public class StockServiceClient:IStockServiceClient
    {
        private readonly HttpClient _httpClient;

        public StockServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public JObject GetTimeSeries()
        {
            return new JObject();
        }
    }
}