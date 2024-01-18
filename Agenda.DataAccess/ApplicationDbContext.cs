using Agenda.BusinessLogic.entities;
using Agenda.DataAccess.entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Agenda.Presentation.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasMany(e => e.Phones)
                .WithOne(e => e.Contact)
                .HasForeignKey("usuarioId") 
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.Emails)
                .WithOne(e => e.Contact)
                .HasForeignKey("usuarioId")
                .IsRequired();
        }
    }
}
