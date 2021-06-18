using Newtonsoft.Json.Linq;

namespace SymbolTracker.Messages
{
    public class ApiDataResponse
    {
        public JObject Data { get; set; }
    }
}