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
            builder.Entity<Event>().HasData(
                new Event { EventId = 1, Type = 1, Cost = 12.5M, DiscountedCost = 10M, DateTime = new DateTime(2020, 3, 15, 5, 20, 0) , EpochWeekendNum = 10, Name = "Weekend10 Enduro", Description = "Event Details"},
                new Event { EventId = 2, Type = 2, Cost = 5.5M, DiscountedCost = 3M, DateTime = new DateTime(2020, 3, 15, 5, 20, 0) , EpochWeekendNum = 10, Name = "Weekend10 Short", Description = "Event Details"},
                new Event { EventId = 3, Type = 1, Cost = 120.5M, DiscountedCost = 100M, DateTime = new DateTime(2020, 4, 16, 5, 20, 0) , EpochWeekendNum = 16, Name = "Weekend16 Enduro", Description = "Event Details"}
            );
        }

    }
}