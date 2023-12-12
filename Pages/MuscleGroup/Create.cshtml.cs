using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using webex.Models;
using webex.Data;

namespace webex.Pages_MuscleGroup
{
    public class CreateModel : PageModel
    {
        private readonly DbContextEx _context;

        public CreateModel(DbContextEx context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MuscleGroup MuscleGroup { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.MuscleGroups == null || MuscleGroup == null)
            {
                return Page();
            }

            _context.MuscleGroups.Add(MuscleGroup);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
