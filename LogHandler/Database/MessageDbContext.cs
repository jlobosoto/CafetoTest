using LoggerHandler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LoggerHandler.Database
{
    public class MessageDbContext: DbContext
    {

        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
    }
}
