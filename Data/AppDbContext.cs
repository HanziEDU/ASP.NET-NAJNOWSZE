using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ContactEntity> Contacts { get; set; } 
        public DbSet<OrganizationEntity> Organizations { get; set; }
        private string Path { get; set; }  

        public AppDbContext() 
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            Path = System.IO.Path.Join(path, "contacts.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"data source={Path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactEntity>()
                .HasOne(e => e.Organization)
                .WithMany(o => o.Contacts)
                .HasForeignKey(e => e.OrganizationId);
            modelBuilder.Entity<OrganizationEntity>()
                .HasData(
                new OrganizationEntity()
                {
                    Id = 101,
                    Name = "WSEI",
                    Description = "Uczelnia Wyższa"
                },
                new OrganizationEntity()
                {
                    Id = 102,
                    Name = "Koło studenckie VR",
                    Description = "Uczelnia Wyższa"
                }
                );

            modelBuilder.Entity<ContactEntity>().HasData(
                new ContactEntity() 
                { 
                    Id = 1, 
                    Name = "Adam", 
                    Email = "Adam@wsei.edu.pl", 
                    Phone = "123456",
                    OrganizationId = 102
                },
                new ContactEntity() 
                { 
                    Id = 2, 
                    Name = "Ewa", 
                    Email = "Ewa@wsei.edu.pl", 
                    Phone = "654321",
                    OrganizationId = 101
                },
                new ContactEntity() 
                { 
                    Id = 3, 
                    Name = "Mikołaj",
                    Email = "Mikołaj@wsei.edu.pl", 
                    Phone = "999999" 
                }
                );
            modelBuilder.Entity<OrganizationEntity>()
                .OwnsOne(o => o.Address)
                .HasData(
                new
                {
                    OrganizationEntityId = 101,
                    City = "Kraków",
                    Street = "Św. Filipa 17",
                    PostalCode = "31-150"
                },
                new
                {
                    OrganizationEntityId = 102,
                    City = "Kraków",
                    Street = "Św. Filipa 17, pok. 12",
                    PostalCode = "31-150"
                }
                );
        }
    }
}
