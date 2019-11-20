using System;
using System.Diagnostics;
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
using Microsoft.AspNetCore.Identity.UI.Services;

using RaceApp.Models;

namespace RaceApp.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public EventController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }
        
        // Registration Section

        public class InputModel
        {
            public Event Event { get; set; }

            [Required]
            public int CarId { get; set; }

            [Display(Name = "Select Car")]
            public IEnumerable<SelectListItem> Cars { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        // GET: Register for id
        public async Task<IActionResult> Register(int? id)
        {   
            var Event = await _context.Events.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            
            List<Tuple<int,int>> carList;

            if (Event.Type == 1){
                carList = await _context.Cars.Where(c => c.ApplicationUserId == user.Id && c.IsEnduro)
                .Select(s => new Tuple<int, int>(s.CarId,s.CarNumber)).ToListAsync();
            }else {
                carList = await _context.Cars.Where(c => c.ApplicationUserId == user.Id && !c.IsEnduro)
                .Select(s => new Tuple<int, int>(s.CarId,s.CarNumber)).ToListAsync();
            }

            Input = new InputModel
            {
                Event = Event,
                Cars = carList.Select(x => new SelectListItem() {
                    Text = x.Item2.ToString(),
                    Value = x.Item1.ToString()
                }),
            };
            return View(Input);
        }

        // POST: Register for id with user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(int id, [Bind("CarId")] InputModel model)
        {
            // if model is valid
            if (ModelState.IsValid)
            {
                // Workaround due to usermanager not supporting joins when finding user by User obj
                var user_temp = await _userManager.GetUserAsync(User);
                var Event = await _context.Events.FindAsync(id);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == user_temp.Id);
                Registration discountCheck = await _context.Registrations.Where(r => r.User.Id == user_temp.Id).FirstOrDefaultAsync();
                
                // get user registrations
                // check if they are already registered with 
                Registration registration = new Registration 
                {
                    DiscountQualified = discountCheck == null ? false : true,
                    ApplicationUserId = user.Id,
                    CarId = model.CarId,
                    EventId = id 
                };
                
                try
                {
                    _context.Add(registration);
                    await _context.SaveChangesAsync();
                    string emailMessage = registration.DiscountQualified ? $"You were successfully registered for {Event.Name}!" +
                        "You got the discounted rate for registering for multiple events in 1 weekend." : $"You were successfully registered for {Event.Name}!";
                    await _emailSender.SendEmailAsync(user.Email, "Registered for Race!", emailMessage);
                } 
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index), TempData["StatusMessage"] = "Event registration confirmed, check email.");
            }
            
            // Something went wrong, return model
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
