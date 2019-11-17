using System.ComponentModel.DataAnnotations;

namespace RaceApp.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        public int CarNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        public string Make { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        public string Model { get; set; }

        // Optional if enduro
        [StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        public string? EngineType { get; set; }

        [StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        public string? EngineBuilder { get; set; }
    }
}