using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public CreateModel(webex.Data.DbContextEx context)
        {
            _context = context;
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

            _context.Workouts.Add(Workout);
            await _context.SaveChangesAsync();

            return Redirect("/Workouts/Edit?id=" + Workout.Id);
        }
    }
}
