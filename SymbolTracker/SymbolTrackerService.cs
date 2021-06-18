using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SymbolTracker.Actors;
using SymbolTracker.Messages;

namespace SymbolTracker
{
    public class SymbolTrackerService:IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SymbolTrackerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bootstrap = BootstrapSetup.Create();
            var di = DependencyResolverSetup.Create(_serviceProvider);
            var actorSystemSetup = bootstrap.And(di);
            var actorSystem = ActorSystem.Create("StockTrackerActorSystem", actorSystemSetup);
            var dependencyResolver = DependencyResolver.For(actorSystem);

            var lookupActorProps = dependencyResolver.Props<SymbolLookupActor>();
            var lookupActor = actorSystem.ActorOf(lookupActorProps, "LookupActor");
            var reporterProps = dependencyResolver.Props<SymbolReporterActor>(lookupActor);
            var reporterAct = actorSystem.ActorOf(reporterProps,"SymbolReporterActor");
            reporterAct.Tell(new RegisterSymbolRequest("AAPL"));
            reporterAct.Tell(new RegisterSymbolRequest("MSFT"));
            reporterAct.Tell(new RegisterSymbolRequest("EUR/USD"));
            reporterAct.Tell(new RegisterSymbolRequest("SBUX"));
            reporterAct.Tell(new RegisterSymbolRequest("NKE"));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}