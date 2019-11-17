using System;
using System.ComponentModel.DataAnnotations;

namespace RaceApp.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public Decimal Cost { get; set; }
        
        [Required]
        public Decimal DiscountedCost { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        // Nullable if event isn't on weekend
        public int? EpochWeekendNum { get; set; }

        // Foreign Key back to users registered
    }
}