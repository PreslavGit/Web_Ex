using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using webex.Data;
using webex.Models;

namespace webex.Pages.Workouts
{
    [Authorize]
    public class CreateModel : PageModel
    {

        private readonly webex.Data.DbContextEx _context;
        [BindProperty]
        public Workout Workout { get; set; } = default!;

        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(webex.Data.DbContextEx context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Workouts == null || Workout == null)
            {
                return Page();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new Exception("User not found");

            Workout.User = user;
            _context.Workouts.Add(Workout);
            await _context.SaveChangesAsync();

            return Redirect("/Workouts/Edit?id=" + Workout.Id);
        }
    }
}
