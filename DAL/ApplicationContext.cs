using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        //todo: manage triggers
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
            ChangeTracker.StateChanged += UpdateChangeTime;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        private void UpdateChangeTime(object? sender, EntityStateChangedEventArgs e)
        {
            var entity = e.Entry.Entity;
            if (entity is null || entity is not IHasTimestamps)
                return;
            (entity as IHasTimestamps).Updated = DateTime.UtcNow;
        }

    }
}