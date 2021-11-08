using LoggerHandler.Database;
using LoggerHandler.Jobs;
using LoggerHandler.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit.Abstractions;

namespace LogHandler.Tests
{
    public class BaseTest
    {
        protected readonly ServiceCollection ServiceCollection;
        protected readonly ITestOutputHelper OutputHelper;
        protected readonly ServiceProvider ServiceProvider;
        protected readonly Mock<ILogger<DBLogger>> _log;
        protected readonly IConfiguration configuration;
        protected readonly MessageDbContext dbContext;
        public BaseTest(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddDbContext<MessageDbContext>(options => options.UseInMemoryDatabase("Test"));
            ServiceCollection.AddSingleton<IMessageService, MessageService>();
            ServiceCollection.AddLogging(builder => { builder.AddDebug(); });
            ServiceProvider = ServiceCollection.BuildServiceProvider();
            _log = new Mock<ILogger<DBLogger>>();

            var inMemorySettings = new Dictionary<string, string> {
                {"DBLogs", "TestConnectionString"},
                {"DestinationFolder", "./Logs/"}
            };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        
    }

    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
}
