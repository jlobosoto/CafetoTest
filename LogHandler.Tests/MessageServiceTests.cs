using FluentAssertions;
using LoggerHandler.Constants;
using LoggerHandler.Database;
using LoggerHandler.DTO.Requests;
using LoggerHandler.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace LogHandler.Tests
{
    public class MessageServiceTests: BaseTest
    {
        public MessageServiceTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            
        }

        [Fact]
        public void LogMessageToConsole()
        {
            var message = new MessageRequestDTO() 
            {
                Destination=Destination.Console,
                MessageDescription="Test Message",
                MessageType=MessageType.Information
            };
            var currentConsoleOut = Console.Out;
            var messageService = new MessageService(_log.Object,configuration,dbContext);

            using (var consoleOutput = new ConsoleOutput())
            {
                messageService.LogMessage(message);

                Assert.Contains(message.MessageDescription, consoleOutput.GetOuput());
            }

            Assert.Equal(currentConsoleOut, Console.Out);
        }

        [Fact]
        public void LogMessageToFile()
        {
            var message = new MessageRequestDTO()
            {
                Destination = Destination.LocalFile,
                MessageDescription = "Test Message",
                MessageType = MessageType.Information
            };

            var inMemorySettings = new Dictionary<string, string> {
                {"DestinationFolder", "./Logs/"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var currentConsoleOut = Console.Out;
            var messageService = new MessageService(_log.Object, configuration,dbContext);

            using (var consoleOutput = new ConsoleOutput())
            {
                messageService.LogMessage(message);

                Assert.Contains("Log Succesfully Saved in local file :", consoleOutput.GetOuput());
            }

            Assert.Equal(currentConsoleOut, Console.Out);
        }
    }
}
