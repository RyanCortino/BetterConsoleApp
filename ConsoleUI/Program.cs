using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setting up configuartion for Serilog
            ConfigurationBuilder builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting.");

            // Setting up Dependency Injections, Configuration, and Logging
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Services go here
                    services.AddTransient<IGreetingService, GreetingService>();
                })
                .UseSerilog()
                .Build();
            
            IGreetingService svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
            svc.Run();

            Console.ReadKey();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Productions"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
