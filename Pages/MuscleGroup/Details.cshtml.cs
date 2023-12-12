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
    public class DetailsModel : PageModel
    {
        private readonly DbContextEx _context;

        public DetailsModel(DbContextEx context)
        {
            _context = context;
        }

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
    }
}
