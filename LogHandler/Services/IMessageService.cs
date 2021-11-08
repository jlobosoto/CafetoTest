using LoggerHandler.DTO.Requests;

namespace LoggerHandler.Services
{
    public interface IMessageService
    {
        void LogMessage(MessageRequestDTO messge);
    }
}