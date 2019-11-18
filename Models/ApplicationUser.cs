using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RaceApp.Models
{
    public class ApplicationUser : IdentityUser<int>
    {	
		[Required]
		[StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        [Display(Name = "First Name")]
		public string First { get; set; } 

		[Required]
		[StringLength(100, ErrorMessage = "Cannot contain more than 100 letters")]
        [Display(Name = "Last Name")]
		public string Last { get; set; }

    }
}