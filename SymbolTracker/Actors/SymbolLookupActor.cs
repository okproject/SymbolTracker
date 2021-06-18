using System;
using Akka.Actor;
using SymbolTracker.Messages;
using SymbolTracker.Services;

namespace SymbolTracker.Actors
{
    public class SymbolLookupActor:ReceiveActor
    {
        private readonly IStockServiceClient _stockServiceClient;

        public SymbolLookupActor(IStockServiceClient stockServiceClient)
        {
            _stockServiceClient = stockServiceClient;
            Receive<ApiDataRequest>(message => HandleApiDataRequest(message));
        }

        private void HandleApiDataRequest(ApiDataRequest message)
        {
            Console.WriteLine("SymbolLookupActor processing ApiDataRequest");
            var result = _stockServiceClient.GetTimeSeries();
            Sender.Tell(new ApiDataResponse(result));
        }
    }
}