using LoggerHandler.DTO.Requests;
using LoggerHandler.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggerHandler.Jobs
{
    public interface IJobLogger
    {
        Task LogMessage(MessageRequestDTO message);
        Task<List<Message>> GetMessages();
    }
    
}
