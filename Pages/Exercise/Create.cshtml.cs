using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using webex.Data;

namespace webex.Pages_Exercise
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
        public Exercise Exercise { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Exercises == null || Exercise == null)
            {
                return Page();
            }

            _context.Exercises.Add(Exercise);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
