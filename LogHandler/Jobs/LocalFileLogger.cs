using LoggerHandler.DTO.Requests;
using LoggerHandler.Exceptions;
using LoggerHandler.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LoggerHandler.Jobs
{
    public class LocalFileLogger : IJobLogger
    {
        private readonly IConfiguration _config;

        public LocalFileLogger(IConfiguration config)
        {
            _config = config;
        }

        public Task<List<Message>> GetMessages()
        {
            throw new NotImplementedException();
        }

        public async Task LogMessage(MessageRequestDTO message)
        {
            try
            {
                string l = string.Empty;
                var logFolder = _config.GetValue<string>("DestinationFolder");
                if (string.IsNullOrEmpty(logFolder)) logFolder = Environment.CurrentDirectory;
                var logFileName = "LogFile" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".txt";
                var logFullFilePath = Path.Combine(logFolder, logFileName);

                if (File.Exists(logFullFilePath))
                {
                    l = File.ReadAllText(logFullFilePath);

                }

                l = l + DateTime.Now.ToShortDateString()+" " + message.MessageType.ToString() + " " + message.MessageDescription + Environment.NewLine;

                if (!Directory.Exists(logFolder)) Directory.CreateDirectory(logFolder);

                File.WriteAllText(logFullFilePath, l);

                Console.WriteLine($"Log Succesfully Saved in local file : {logFullFilePath}");
            }
            catch (LoggerException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
