using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using RaceApp.Models;

namespace RaceApp.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(await _context.Events.ToListAsync()); //FIXME
        }

        // Registration Section
        public class EventRegisterViewModel : Event
        {
            public int CarId { get; set; }

            [Required]
            [Display(Name = "Select Car")]
            public IEnumerable<SelectListItem> Cars { get; set; }
        }

        public EventRegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        // GET: Register for id
        public async Task<IActionResult> Register(int? id)
        {   
            var Event = _context.Events.Where(e => e.EventId == id).First();
            
            Input.Name = Event.Name;
            Input.DateTime = Event.DateTime;
            Input.Cost = Event.Cost;
            Input.DiscountedCost = Event.DiscountedCost;

            var user = await _userManager.GetUserAsync(User);
            if (Event.Type == 1){
                Input.Cars = await _context.Cars.Where(c => c.ApplicationUserId == user.Id && c.IsEnduro).Select(s => new SelectListItem { Value = s.CarNumber.ToString(), Text = s.CarId.ToString() }).ToListAsync();
            }else {
                Input.Cars = await _context.Cars.Where(c => c.ApplicationUserId == user.Id && !c.IsEnduro).Select(s => new SelectListItem { Value = s.CarNumber.ToString(), Text = s.CarId.ToString() }).ToListAsync();
            }
            return View(Input);
        }

    }
}
