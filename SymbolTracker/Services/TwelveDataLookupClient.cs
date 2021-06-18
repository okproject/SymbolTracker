using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace SymbolTracker.Services
{
    public class TwelveDataLookupClient:ISymbolLookupClient
    {
        private readonly HttpClient _httpClient;

        public TwelveDataLookupClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public JObject GetTimeSeries()
        {
            return new JObject();
        }
    }
}