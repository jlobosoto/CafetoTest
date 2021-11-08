using LoggerHandler.Constants;
using LoggerHandler.Database;
using LoggerHandler.DTO.Requests;
using LoggerHandler.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections;
using System.IO;

// DI, Serilog, Settings

namespace LoggerHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IMessageService, MessageService>();
                    services.AddDbContext<MessageDbContext>();
                })
                .UseSerilog()
                .Build();


            Console.WriteLine("Please write your log message:");
            var description = Console.ReadLine();

            Console.WriteLine("Please Type the Message Type you want to log: 0->Information  1->Warning  2->Error  3->Critical");
            MessageType messageType = default;
            var messageTypeString = Enum.TryParse(Console.ReadLine(), out messageType);

            Console.WriteLine("Please Determine Log Destination, If you want multiple destinations you should write each one separated by underscores: 0->Console  1->Local File  2->Database");

            
            IEnumerable destinations = Console.ReadLine().Split("_");

            foreach (var tempDestination in destinations)
            {
                Destination destination = default;
                var destinationString = Enum.TryParse(tempDestination.ToString(), out destination);

                var message = new MessageRequestDTO()
                {
                    Destination = destination,
                    MessageDescription = description,
                    MessageType = messageType
                };
                var svc = ActivatorUtilities.CreateInstance<MessageService>(host.Services);
                svc.LogMessage(message);
            }

        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}

