using System.ComponentModel.DataAnnotations;

namespace RaceApp.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationId { get; set; }

        [Required]
        public bool DiscountQualified { get; set; }

        // FK -> User
        public int ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        // FK -> Car
        public int CarId { get; set; }
        public Car Car { get; set; }

        // FK -> Event
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}