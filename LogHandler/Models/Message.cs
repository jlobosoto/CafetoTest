using System.ComponentModel.DataAnnotations;

namespace LoggerHandler.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string MessageDescription { get; set; }
        public int Destination { get; set; }
        public int MessageType { get; set; }
    }
}
