using System;
using Akka.Actor;
using SymbolTracker.Messages;
using SymbolTracker.Services;

namespace SymbolTracker.Actors
{
    public class SymbolLookupActor:ReceiveActor
    {
        private readonly ISymbolLookupClient _symbolLookupClient;

        public SymbolLookupActor(ISymbolLookupClient symbolLookupClient)
        {
            _symbolLookupClient = symbolLookupClient;
            Receive<ApiDataRequest>(message => HandleApiDataRequest(message));
        }

        private void HandleApiDataRequest(ApiDataRequest message)
        {
            Console.WriteLine("SymbolLookupActor processing ApiDataRequest");
            var result = _symbolLookupClient.GetTimeSeries();
            Sender.Tell(new ApiDataResponse(result));
        }
    }
}