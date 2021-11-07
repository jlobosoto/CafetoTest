using LoggerHandler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LoggerHandler.Database
{
    public class MessageDbContext: DbContext
    {
        private readonly IConfiguration _config;

        public MessageDbContext(IConfiguration config)
        {

            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetValue<string>("DBLogs"))
                .EnableSensitiveDataLogging(true);      
        }
        public DbSet<Message> Messages { get; set; }
    }
}
