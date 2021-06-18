using Newtonsoft.Json.Linq;

namespace SymbolTracker.Messages
{
    public class ApiDataResponse
    {
        public ApiDataResponse(JObject data)
        {
            Data = data;
        }

        public JObject Data { get; private set; }
    }
}