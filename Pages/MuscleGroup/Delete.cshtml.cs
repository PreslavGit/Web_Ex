using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webex.Models;
using webex.Data;

namespace webex.Pages_MuscleGroup
{
    public class DeleteModel : PageModel
    {
        private readonly DbContextEx _context;

        public DeleteModel(DbContextEx context)
        {
            _context = context;
        }

        [BindProperty]
      public MuscleGroup MuscleGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MuscleGroups == null)
            {
                return NotFound();
            }

            var musclegroup = await _context.MuscleGroups.FirstOrDefaultAsync(m => m.Id == id);

            if (musclegroup == null)
            {
                return NotFound();
            }
            else 
            {
                MuscleGroup = musclegroup;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MuscleGroups == null)
            {
                return NotFound();
            }
            var musclegroup = await _context.MuscleGroups.FindAsync(id);

            if (musclegroup != null)
            {
                MuscleGroup = musclegroup;
                _context.MuscleGroups.Remove(MuscleGroup);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
