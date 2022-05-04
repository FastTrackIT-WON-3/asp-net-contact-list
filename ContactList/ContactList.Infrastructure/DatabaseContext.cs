using ContactList.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ContactListEntity> ContactListEntry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactListEntity>()
                .ToTable("ContactLists")
                .HasKey(cl => cl.Id);

            modelBuilder.Entity<ContactListEntity>()
                .Property(cl => cl.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<ContactListEntity>()
                .Property(cl => cl.Address)
                .HasMaxLength(500);

            modelBuilder.Entity<ContactListEntity>()
                .Property(cl => cl.PhoneNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<ContactListEntity>()
                .Property(cl => cl.Email)
                .HasMaxLength(50);
        }
    }
}
