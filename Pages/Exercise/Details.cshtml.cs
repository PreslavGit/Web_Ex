using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webex.Data;

namespace webex.Pages_Exercise
{
    public class DetailsModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;

        public DetailsModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }

      public Exercise Exercise { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Exercises == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            else 
            {
                Exercise = exercise;
            }
            return Page();
        }
    }
}
