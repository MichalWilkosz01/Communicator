using Communicator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Communicator.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Correspondence>()
            .HasOne(c => c.Sender)
            .WithMany()
            .HasForeignKey(c => c.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Friendship>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Friendship>()
           .HasOne(c => c.Friend)
           .WithMany()
           .HasForeignKey(c => c.FriendId)
           .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Correspondence> Correspondences { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
    }
}
