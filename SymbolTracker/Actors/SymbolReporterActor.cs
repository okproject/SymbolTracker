using System;
using System.Collections.Generic;
using System.Diagnostics;
using Akka.Actor;
using SymbolTracker.Messages;

namespace SymbolTracker.Actors
{
    public class SymbolReporterActor:ReceiveActor
    {
        private List<IActorRef>  _subscribers { get; set; }
        private ICancelable _getPriceScheduler;
        private IActorRef _lookupActor;
        


        public SymbolReporterActor(IActorRef lookupActor)
        {
            Receive<ApiDataResponse>(mesasage => HandleApiDateResponse(mesasage));
            _lookupActor = lookupActor;
            _subscribers = new List<IActorRef>();
        }

        private void HandleApiDateResponse(ApiDataResponse mesasage)
        {
            //Tell subscribers
            foreach (var actorRef in _subscribers)
            {
                actorRef.Tell(mesasage.Data);                
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
    }
}