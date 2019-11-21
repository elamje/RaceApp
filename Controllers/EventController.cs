using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
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

            List<Tuple<int, int>> carList;

            if (Event.Type == 1)
            {
                carList = await _context.Cars.Where(c => c.ApplicationUserId == user.Id && c.IsEnduro)
                    .Select(s => new Tuple<int, int>(s.CarId, s.CarNumber)).ToListAsync();
            }
            else
            {
                carList = await _context.Cars.Where(c => c.ApplicationUserId == user.Id && !c.IsEnduro)
                    .Select(s => new Tuple<int, int>(s.CarId, s.CarNumber)).ToListAsync();
            }

            Input = new InputModel
            {
                Event = Event,
                Cars = carList.Select(x => new SelectListItem()
                {
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
                Registration alreadyRegisteredCheck = await _context.Registrations.Where(r => r.User.Id == user_temp.Id && r.EventId == id)
                    .FirstOrDefaultAsync();
                Car carValid = await _context.Cars.Where(c => c.ApplicationUserId == user_temp.Id && c.CarId == model.CarId)
                    .FirstOrDefaultAsync();

                Registration discountCheck = await _context.Registrations.Where(r => r.User.Id == user_temp.Id 
                    && r.Event.Type != Event.Type && r.Event.EpochWeekendNum == Event.EpochWeekendNum).FirstOrDefaultAsync();


                if (carValid == null)
                {
                    return RedirectToAction(nameof(Index), TempData["StatusMessage"] = "Oops, that isn't a valid car.");
                }
                if (alreadyRegisteredCheck != null)
                {
                    return RedirectToAction(nameof(Index), TempData["StatusMessage"] = "You've already registered for this.");
                }

                // get user registrations
                // check if they are already registered with 
                Registration registration = new Registration
                {
                    DiscountQualified = discountCheck == null ? false : true,
                    ApplicationUserId = user.Id,
                    CarId = model.CarId,
                    EventId = id
                };

                string emailMessage = registration.DiscountQualified ? $"You were successfully registered for {Event.Name}!" +
                        " You got the discounted rate for signing up for an Enduro and Short race on the same weekend." 
                        : $"You were successfully registered for {Event.Name}!";

                try
                {
                    _context.Add(registration);
                    await _context.SaveChangesAsync();
                    await _emailSender.SendEmailAsync(user.Email, "Registered for Race!", emailMessage);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index), TempData["StatusMessage"] = emailMessage);
            }

            // Something went wrong, return model
            return View(model);
        }

        // POST: Event/Registration/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deregister(int id)
        {
            // Lookup by user and event
            var user = await _userManager.GetUserAsync(User);
            var registration = await _context.Registrations.Include(r => r.Event).Where(r => r.EventId == id 
                && r.ApplicationUserId == user.Id).FirstOrDefaultAsync();
                
            if (registration == null)
            {
                return NotFound();
            }
            else
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), TempData["StatusMessage"] = "You were unregistered successfully.");
            }
        }

    }
}
