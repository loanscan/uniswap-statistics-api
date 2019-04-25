using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Uniswap.Fetchers.Composition;

namespace Uniswap.Fetchers
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
            if (!string.IsNullOrEmpty(environment))
            {
                configurationBuilder = configurationBuilder.AddJsonFile($"appsettings.{environment}.json");
            }
            
            var plainConfig = configurationBuilder.Build();
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(plainConfig)
                .Enrich.WithProperty("Service", "Uniswap.Fetchers")
                .CreateLogger();

            var options = plainConfig.Get<Options.Options>();

            var host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder
                        .RegisterModule(new Web3Module(options.NodeUrl));
                    
                    builder
                        .RegisterModule(new MongoModule(options.Db));

                    builder
                        .RegisterModule(new InfraModule(options.TokenInfoProvider));

                    builder
                        .RegisterModule(new FactoryModule(options.Contracts.FactoryAddress, options.Fetcher));

                    builder
                        .RegisterModule(new ExchangeModule(options.ExchangeFetcher, options.ExchangeThemes));
                })
                .UseConsoleLifetime()
                .UseSerilog()
                .Build();
            
            try
            {
                Log.Information("Host is running...");

                await host.RunAsync();

                Log.Information("Host is stopped");
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Unknown error occurred");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}