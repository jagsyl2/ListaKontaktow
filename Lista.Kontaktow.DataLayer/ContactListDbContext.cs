using ListaKontaktow.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace ListaKontaktow.DataLayer
{
    public interface IContactListDbContext : IDisposable
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Subcategory> Subcategories { get; set; } 
        DbSet<User> Users { get; set; }
        DatabaseFacade Database { get; }

        int SaveChanges();
    }

    public class ContactListDbContext : DbContext, IContactListDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ContactListDB;Trusted_Connection=True");
        }
    }
}
