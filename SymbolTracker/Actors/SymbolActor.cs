using System;
using System.IO.IsolatedStorage;
using Akka.Actor;
using SymbolTracker.Messages;

namespace SymbolTracker.Actors
{
    public class SymbolActor:ReceiveActor
    {
        public string SymbolName { get; set; }

        public SymbolActor(string symbolName)
        {
            SymbolName = symbolName;
            Receive<ApiDataResponse>(message => HandleApiDataResponse(message));
        }

        private void HandleApiDataResponse(ApiDataResponse message)
        {
            //TODO:parse message, select related data, calculate average
            Sender.Tell(new SymbolAverageResponse()
            {
                Average = 1,
                Date = DateTime.Now,
                SymbolName = SymbolName
            });
        }
    }
}