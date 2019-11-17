using System;
using System.ComponentModel.DataAnnotations;

namespace RaceApp.Models
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [EmailAddress]
        public string Sender { get; set; }

        public Boolean SendSuccess { get; set; }

        // Foreign Key back to recipient User account

    }
}