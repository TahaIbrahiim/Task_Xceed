using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options) { }

        public AppDbContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }



        // Seeding with data
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Laptops" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Food" }
            );
        }
    }
}
