using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RaceApp.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [Display(Name = "Car Number")]
        public int CarNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        public string Make { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Is Car Enduro?")]
        public bool IsEnduro { get; set; }

#nullable enable
        // Optional if enduro
        [StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        [Display(Name = "Engine Type")]
        [CarType]
        public string? EngineType { get; set; }

        [StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        [Display(Name = "Engine Builder Name")]
        [CarType]
        public string? EngineBuilder { get; set; }
#nullable disable

        // FK -> User
        public int ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}