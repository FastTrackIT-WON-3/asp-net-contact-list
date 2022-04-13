using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactList.Models;

namespace ContactList.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ContactList.Models.ContactListEntry> ContactListEntry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactListEntry>()
                .ToTable("ContactLists")
                .HasKey(cl => cl.Id);

            modelBuilder.Entity<ContactListEntry>()
                .Property(cl => cl.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<ContactListEntry>()
                .Property(cl => cl.Address)
                .HasMaxLength(500);

            modelBuilder.Entity<ContactListEntry>()
                .Property(cl => cl.PhoneNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<ContactListEntry>()
                .Property(cl => cl.Email)
                .HasMaxLength(50);
        }
    }
}
