using Microsoft.EntityFrameworkCore;

namespace ChatUygulamasıBackend.Models
{
    public class ChatContext : DbContext
    {
        // Bu parametreli constructor EF Core için zorunludur!
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}