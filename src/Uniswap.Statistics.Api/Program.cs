using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Uniswap.Statistics.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
            if (!string.IsNullOrEmpty(environment))
            {
                configurationBuilder = configurationBuilder.AddJsonFile($"appsettings.{environment}.json");
            }

            var configuration = configurationBuilder.Build();
            var options = configuration.Get<Options.Options>();
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Service", "Uniswap.Statistics.Api")
                .CreateLogger();
            
            try
            {
                Log.Warning("Starting web host...");
                CreateWebHostBuilder(args, options).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Web Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, Options.Options options) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton(options);
                    services.AddAutofac();
                })
                .UseStartup<Startup>()
                .UseSerilog();
    }
}