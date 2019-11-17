using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RaceApp.Models
{
    public class ApplicationUser : IdentityUser<int>
    {	
		[Required]
		[StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
		public string First { get; set; } 

		[Required]
		[StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
		public string Last { get; set; }

		[Required]
		[EmailAddress]
        [StringLength(100, ErrorMessage = "Email cannot be more than 100 characters")]
		public string EmailAddress { get; set; }
    }
}