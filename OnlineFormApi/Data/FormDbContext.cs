using Microsoft.EntityFrameworkCore;
using OnlineForm.Data.Entities;
using OnlineFormApi.Data;
using OnlineFormApi.Data.Entities;

namespace OnlineForm.Services
{
    public class FormDbContext : DbContext
    {
        public FormDbContext(DbContextOptions<FormDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasIndex(x => x.RoleName).IsUnique();
            RoleSeeder.SeedRoles(modelBuilder);
        }
        public DbSet<Person> Person { get; set; }

        public DbSet<Role> Roles { get; set; }

    }
}
