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
    public class DetailsModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;

        public DetailsModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }
[BindProperty]  
        public Workout Workout { get; set; } = default!; 
         public List<Models.Exercise> Exercises { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Workouts == null)
            {
                return NotFound();
            }

            
            Exercises = _context.Exercises.ToList();
            var workout = await getWorkoutMapped(id);
            if (workout == null)
            {
                return NotFound();
            }
            else 
            {
                Workout = workout;
            }
            return Page();
        }

       private async Task<Workout> getWorkoutMapped(int? id)
        {
            var wo = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (wo == null) throw new Exception("Enity not found");

            return wo;
        }
    }
}
