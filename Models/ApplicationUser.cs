using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RaceApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
		
		[Required]
		[StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
		public string First { get; set; } 

		[Required]
		[StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
		public string Last { get; set; }

		[Required]
		[EmailAddress]
		public string EmailAddress { get; set; }
    }
}