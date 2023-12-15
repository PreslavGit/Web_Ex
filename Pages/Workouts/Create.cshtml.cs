using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using webex.Data;

namespace webex.Pages.Workouts
{
    public class CreateModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;

        public CreateModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Workout Workout { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Workouts == null || Workout == null)
            {
                return Page();
            }

            _context.Workouts.Add(Workout);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
