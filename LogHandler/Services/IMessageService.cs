using LoggerHandler.DTO.Requests;

namespace LoggerHandler
{
    public interface IMessageService
    {
        void LogMessage(MessageRequestDTO messge);
    }
}