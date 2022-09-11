using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=DbPhoneList;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Person> People { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(model =>
            {
                model.HasKey(p => p.Id);
                model.Property(p => p.Name).HasColumnName("Firstname");
                model.Property(p => p.Surname).HasColumnName("Lastname");
                model.Property(p => p.Email).HasColumnName("Email");
                model.Property(p => p.Number).HasColumnName("Mobile");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
