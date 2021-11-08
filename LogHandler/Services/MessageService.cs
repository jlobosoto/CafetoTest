using LoggerHandler.Database;
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
        private readonly MessageDbContext _dbContext;

        public MessageService(ILogger<DBLogger> log, IConfiguration config, MessageDbContext dbContext)
        {
            _log = log;
            _config = config;
            _dbContext = dbContext;
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
                        logger = new DBLogger(_log, _dbContext);
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
