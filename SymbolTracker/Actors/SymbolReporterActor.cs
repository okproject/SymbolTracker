using System;
using System.Collections.Generic;
using System.Diagnostics;
using Akka.Actor;
using SymbolTracker.Messages;

namespace SymbolTracker.Actors
{
    public class SymbolReporterActor:ReceiveActor
    {
        private Dictionary<string, IActorRef>  _subscribers { get; set; }
        private ICancelable _getPriceScheduler;
        private IActorRef _lookupActor;
        


        public SymbolReporterActor(IActorRef lookupActor)
        {
            Receive<ApiDataResponse>(mesasage => HandleApiDataResponse(mesasage));
            Receive<RegisterSymbolRequest>(mesasage => HandleRegisterSymbolRequest(mesasage));
            _lookupActor = lookupActor;
            _subscribers = new Dictionary<string, IActorRef>();
        }

        private void HandleApiDataResponse(ApiDataResponse mesasage)
        {
            //Tell subscribers
            foreach (var actorRef in _subscribers)
            {
                actorRef.Value.Tell(mesasage.Data);                
            }
        }

        protected override void PreStart()
        {
            _getPriceScheduler = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                TimeSpan.FromSeconds(4),
                TimeSpan.FromSeconds(4),
                _lookupActor,
                new ApiDataRequest(),
                Self);
            // base.PreStart();
        }

        protected override void PostStop()
        {
            _getPriceScheduler.Cancel(false);
            base.PostStop();

            //TODO: use become and unhandled when necessary
            // Become(_=> {}); // update current reciever
            //Unhandled(); // send event to system event stream if unhandled
            // _priceLookupChildActor.Tell(PoisonPill.Instance); // stop the Actor
        }
        
        private void HandleRegisterSymbolRequest(RegisterSymbolRequest message)
        {
            var isChildActorExists = _subscribers.ContainsKey(message.StockSymbol);
            if (!isChildActorExists)
            {
                // var newStockActor = Context.ActorOf(Props.Create(() => new StockActor(message.StockSymbol)), "StockActor_" + message.StockSymbol);
                var props = Props.Create<SymbolActor>(message.StockSymbol);
                var symbolActor = Context.ActorOf(props, "SymbolActor_" + message.StockSymbol);
                
                _subscribers.Add(message.StockSymbol, symbolActor);
            }
        }
    }
}