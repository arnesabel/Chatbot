using Chatbot.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Infrastructure.Data
{
    public sealed class AppDbContext
        : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureUser(builder);
            ConfigureChatMessage(builder);
        }

        private static void ConfigureUser(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(u => u.DisplayName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(u => u.Messages)
                      .WithOne(m => m.User)
                      .HasForeignKey(m => m.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureChatMessage(ModelBuilder builder)
        {
            builder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("ChatMessages");

                entity.HasKey(m => m.Id);

                entity.Property(m => m.Message)
                      .IsRequired()
                      .HasMaxLength(2000);

                entity.Property(m => m.SendAt)
                      .IsRequired();

                entity.HasIndex(m => m.SendAt);

                entity.HasOne(m => m.User)
                      .WithMany(u => u.Messages)
                      .HasForeignKey(m => m.UserId)
                      .IsRequired();
            });
        }
    }
}
