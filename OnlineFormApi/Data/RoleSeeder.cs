

using Microsoft.EntityFrameworkCore;
using OnlineFormApi.Data.Entities;

namespace OnlineFormApi.Data
{
    public static class RoleSeeder
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(new Role() 
                { 
                    RoleName = "System Administrator 1", 
                    CreatedOn = DateTime.Now,
                    Id = -1
                });
            modelBuilder.Entity<Role>()
            .HasData(new Role() 
            { 
                RoleName = "Role Administrator", 
                CreatedOn = DateTime.Now ,
                Id = -2
            });
            modelBuilder.Entity<Role>()
            .HasData(new Role() 
            { 
                RoleName = "Active Directory Administrator", 
                CreatedOn = DateTime.Now,
                Id = -3
            });
            modelBuilder.Entity<Role>()
            .HasData(new Role() 
            { 
                RoleName = "Page Administrator", 
                CreatedOn = DateTime.Now,
                Id = -4
            });
            modelBuilder.Entity<Role>()
            .HasData(new Role() 
            { 
                RoleName = "System Auditor", 
                CreatedOn = DateTime.Now,
                Id = -5
            });
            modelBuilder.Entity<Role>()
            .HasData(new Role() 
            { 
                RoleName = "Global Auditor",
                CreatedOn = DateTime.Now,
                Id= -6
            });
        }

    }
}
