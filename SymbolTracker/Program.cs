using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SymbolTracker.Actors;
using SymbolTracker.Services;

namespace SymbolTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient<SymbolTrackerService>();
                    services.AddSingleton<ISymbolLookupClient, TwelveDataLookupClient>();
                    services.AddTransient<SymbolLookupActor>();
                    services.AddHostedService<SymbolTrackerService>();
                });
    }
}