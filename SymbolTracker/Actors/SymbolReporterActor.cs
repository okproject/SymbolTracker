using System;
using System.Collections.Generic;
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
            _lookupActor = lookupActor;
            _subscribers = new List<IActorRef>();
            Receive<ApiDataResponse>(mesasage => HandleApiDateResponse(mesasage));
        }

        private void HandleApiDateResponse(ApiDataResponse mesasage)
        {
            //Tell subscribers
            foreach (var actorRef in _subscribers)
            {
                actorRef.Tell(mesasage.Data);
                Console.WriteLine("Sent to :"+actorRef.Path);
            }

            
        }

        protected override void PreStart()
        {
            _getPriceScheduler = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
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