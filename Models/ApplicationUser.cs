using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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

        public ICollection<Car> Cars { get; set; }
        public ICollection<Email> Emails { get; set; }

        public ICollection<EventUser> EventUsers { get; set; }
    }
}