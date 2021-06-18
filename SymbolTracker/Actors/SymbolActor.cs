using System;
using System.Linq;
using Akka.Actor;
using SymbolTracker.Messages;

namespace SymbolTracker.Actors
{
    public class SymbolActor : ReceiveActor
    {
        public string SymbolName { get; set; }

        public SymbolActor(string symbolName)
        {
            SymbolName = symbolName;
            Receive<ApiDataResponse>(message => HandleApiDataResponse(message));
        }

        private void HandleApiDataResponse(ApiDataResponse message)
        {
            var stockSymbols =
                message.Data.FirstOrDefault(x => x?.Symbol?.Trim().ToLower() == SymbolName?.Trim().ToLower());
            var avg = stockSymbols?.Values?.Average(a => a.Close) ?? 0;
            avg = Decimal.Round(avg, 5);
            Sender.Tell(new SymbolAverageResponse(SymbolName, avg, DateTime.Now));
        }
    }
}