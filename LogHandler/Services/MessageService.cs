using LoggerHandler.DTO.Requests;
using LoggerHandler.Exceptions;
using LoggerHandler.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace LoggerHandler.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<DBLogger> _log;
        private readonly IConfiguration _config;

        public MessageService(ILogger<DBLogger> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void LogMessage(MessageRequestDTO message)
        {
            try
            {
                IJobLogger logger = default;

                switch (message.Destination)
                {
                    case Constants.Destination.Console:
                        logger = new ConsoleLogger();
                        break;
                    case Constants.Destination.LocalFile:
                        logger = new LocalFileLogger(_config);
                        break;
                    case Constants.Destination.DataBase:
                        logger = new DBLogger(_log, _config);
                        break;
                    default:
                        break;
                }

                logger.LogMessage(message);
            }
            catch (LoggerException e)
            {

                Console.WriteLine(e.Message, ConsoleColor.Red);
            }
        }
    }
}
