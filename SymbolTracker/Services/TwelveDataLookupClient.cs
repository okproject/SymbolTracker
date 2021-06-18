using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SymbolTracker.Services
{
    public class TwelveDataLookupClient : ISymbolLookupClient
    {
        //25556349db984abd8e6a7a6062a733b4
        private string _apiUrl { get; set; } =
            "https://api.twelvedata.com/time_series?symbol=AAPL,MSFT,EUR/USD,SBUX,NKE&interval=1min&apikey=demo&source=docs";

        private readonly HttpClient _httpClient;

        public TwelveDataLookupClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StockSymbol>> GetTimeSeries()
        {
            List<StockSymbol> stockSymbols = new List<StockSymbol>();
            var httpResponse = await _httpClient.GetAsync(_apiUrl);
            var str = await httpResponse.Content.ReadAsStringAsync();
            var result = JObject.Parse(str);

            foreach (var jo in result)
            {
                var item = result[jo.Key];
                var res = new StockSymbol();
                res.Symbol = jo.Key;
                res.Meta = JsonConvert.DeserializeObject<Meta>(item["meta"].ToString());
                res.Status = item["status"].ToString();
                res.Values = JsonConvert.DeserializeObject<List<Value>>(item["values"].ToString());
                stockSymbols.Add(res);
            }

            return stockSymbols;
        }
    }
}