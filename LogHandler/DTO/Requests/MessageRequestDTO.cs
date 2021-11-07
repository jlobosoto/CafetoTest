using LoggerHandler.Constants;

namespace LoggerHandler.DTO.Requests
{ 
    public class MessageRequestDTO
    {
        public string MessageDescription { get; set; }
        public Destination Destination { get; set; }
        public MessageType MessageType { get; set; }
    }
}
