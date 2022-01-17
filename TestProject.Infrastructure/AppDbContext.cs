using Microsoft.EntityFrameworkCore;

using TestProject.Domain.Models;

namespace TestProject.Infrastructure
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
                .HasMany(x => x.Accounts)
                .WithOne(x => x.Incident)
                .HasForeignKey(x => x.IncidentName);

            modelBuilder
                .Entity<Incident>()
                .HasKey(x => x.Name);

            modelBuilder
                .Entity<Account>()
                .HasMany(x => x.Contacts)
                .WithOne(x => x.Account);

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
