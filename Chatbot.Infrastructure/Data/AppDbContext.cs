using Chatbot.Core.Constants;
using Chatbot.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatbot.Infrastructure.Data
{

    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public const string CONNECTION_STRING_NAME = "DefaultConnection";

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(e => e.DisplayName)
               .IsRequired();

            modelBuilder.Entity<User>().HasIndex(e => e.DisplayName)
                .IsUnique();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }
        private void SeedData(ModelBuilder modelBuilder)
        {            
            var botUser = new User
            {
                Id = HubConstants.CHAT_BOT_ID,
                Email = HubConstants.CHAT_BOT_MAIL,
                EmailConfirmed = true,
                UserName = HubConstants.CHAT_BOT_MAIL,
                DisplayName = HubConstants.CHAT_BOT_NAME
            };

            var ph = new PasswordHasher<User>();
            botUser.PasswordHash = ph.HashPassword(botUser, "lMWHr0xuVfC^");

            var builder = modelBuilder.Entity<User>();
            builder.HasData(botUser);
        }
    }
}
