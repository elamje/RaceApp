using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RaceApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Event> Events { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Car>().HasData(
                new Car { CarId = 1, CarNumber = 96, Make = "Ford", Model = "GT"},
                new Car { CarId = 2, CarNumber = 12, Make = "Ferrari", Model = "Enzo", EngineType = "Gas", EngineBuilder = "Carol Shelby" } 
            );
            builder.Entity<Event>().HasData(
                new Event { EventId = 1, Type = 1, Cost = 12.5M, DiscountedCost = 10M, DateTime = new DateTime(2020, 3, 15, 5, 20, 0) }
            );
        }

    }
}