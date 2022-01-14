using Microsoft.EntityFrameworkCore;

using TestProject.Domain.Models;

namespace TestProject.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Incident>()
                .HasOne(x => x.Account)
                .WithMany(x => x.Incidents);

            modelBuilder
                .Entity<Incident>()
                .HasKey(x => x.Name);

            modelBuilder
                .Entity<Account>()
                .HasOne(x => x.Contact)
                .WithMany(x => x.Accounts);

            modelBuilder
                .Entity<Account>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder
                .Entity<Contact>()
                .HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}
