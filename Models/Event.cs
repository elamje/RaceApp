using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RaceApp.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Race Type")]
        public int Type { get; set; }

        [Required]
        public Decimal Cost { get; set; }
        
        [Required]
        [Display(Name = "Discounted Cost")]
        public Decimal DiscountedCost { get; set; }

        [Required]
        [Display(Name = "Event Time")]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Event Name cannot be more than 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Event Description cannot be more than 1000 characters")]
        [Display(Name = "About")]
        public string Description { get; set; }

        // Nullable if event isn't on weekend, Weekend Count since Epoch
        public int? EpochWeekendNum { get; set; }

        // public ICollection<EventUser> EventUsers { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}