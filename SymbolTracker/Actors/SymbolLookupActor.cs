using Akka.Actor;
using SymbolTracker.Messages;
using SymbolTracker.Services;

namespace SymbolTracker.Actors
{
    public class SymbolLookupActor : ReceiveActor
    {
        private readonly ISymbolLookupClient _symbolLookupClient;

        public SymbolLookupActor(ISymbolLookupClient symbolLookupClient)    
        {
            _symbolLookupClient = symbolLookupClient;
            Receive<ApiDataRequest>(message => HandleApiDataRequest(message));
        }

        private void HandleApiDataRequest(ApiDataRequest message)
        {
            var result =  _symbolLookupClient.GetTimeSeries().Result;
            //TODO: do not use async - await pattern
            Sender.Tell(new ApiDataResponse(result));
        }
    }
}