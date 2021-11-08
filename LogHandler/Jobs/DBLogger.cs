using LoggerHandler.Database;
using LoggerHandler.DTO.Requests;
using LoggerHandler.Exceptions;
using LoggerHandler.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggerHandler.Jobs
{

    public class DBLogger : IJobLogger
    {
        private readonly MessageDbContext _dbContext;
        private readonly ILogger<DBLogger> _log;

        public DBLogger(ILogger<DBLogger> log, MessageDbContext dbContext)
        {
            _dbContext = dbContext;
            _log = log;
        }
        public async Task LogMessage(MessageRequestDTO message)
        {
            try
            {
                var record = message.Adapt<Message>();
                
                _dbContext.Messages.Add(record);
                _dbContext.SaveChanges();

                Console.WriteLine("Log Saved in DB.");
            }
            catch (LoggerException e)
            {

                throw e;
            }
        }

        //Not implemented due to test requirements
        public async Task<List<Message>> GetMessages()
        {
            var messages = await _dbContext.Messages.ToListAsync();
            return messages;
        }
    }
}
