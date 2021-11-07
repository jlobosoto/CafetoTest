using LoggerHandler.Constants;
using LoggerHandler.DTO.Requests;
using LoggerHandler.Exceptions;
using LoggerHandler.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggerHandler.Jobs
{
    public class ConsoleLogger : IJobLogger
    {
        public Task<List<Message>> GetMessages()
        {
            var response = new List<Message>() { new Message { MessageDescription = "With this method is not possible to retreive message, Sorry." } };
            
            return Task.Run(()=>response);
        }

        public async Task LogMessage(MessageRequestDTO message)
        {
            try
            {
                
                switch (message.MessageType)
                {
                    case MessageType.Information:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case MessageType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case MessageType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case MessageType.Critical:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    default:
                        break;
                }
                
                Console.WriteLine(DateTime.Now.ToShortDateString() +" "+ message.MessageDescription);
               
            }
            catch (LoggerException e)
            {

                throw e;
            }

        }
    }
}
