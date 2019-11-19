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
        public DbSet<EventUser> EventUsers { get; set; }
        
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

            // Configure Id for Application User
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("ApplicationUser");
                entity.Property(e => e.Id).HasColumnName("ApplicationUserId");
            });

            // Join Table for Event and User Many - Many
            builder.Entity<EventUser>().HasKey(eu => new { eu.EventId, eu.ApplicationUserId });
            
            builder.Entity<EventUser>()
                .HasOne<Event>(eu => eu.Event)
                .WithMany(e => e.EventUsers)
                .HasForeignKey(eu => eu.EventId);

            builder.Entity<EventUser>()
                .HasOne<ApplicationUser>(eu => eu.ApplicationUser)
                .WithMany(e => e.EventUsers)
                .HasForeignKey(eu => eu.ApplicationUserId);

            // Seed Data
            // builder.Entity<ApplicationUser>().HasData(
            //     new ApplicationUser{ Id = 1, First = "Test", Last = "Test", Email = "test@mailinator.com", NormalizedEmail = "TEST@MAILINATOR.COM",}
            // );  
            // builder.Entity<Car>().HasData(
            //     new Car { CarId = 1, CarNumber = 96, Make = "Ford", Model = "GT", ApplicationUserId = 1},
            //     new Car { CarId = 2, CarNumber = 12, Make = "Ferrari", Model = "Enzo", EngineType = "Gas", EngineBuilder = "Carol Shelby", ApplicationUserId = 1} 
            // );
            // builder.Entity<Event>().HasData(
            //     new Event { EventId = 1, Type = 1, Cost = 12.5M, DiscountedCost = 10M, DateTime = new DateTime(2020, 3, 15, 5, 20, 0) }
            // );
        }

    }
}