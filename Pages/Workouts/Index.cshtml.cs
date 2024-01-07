using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webex.Data;
using webex.Models;

namespace webex.Pages.Workouts
{
    public class IndexModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;

        public IndexModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Models.Workout> Workout { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Workouts != null)
            {
                Workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                    .ThenInclude(ex => ex.Exercise)
                .ToListAsync();
            }
        }
    }
}
