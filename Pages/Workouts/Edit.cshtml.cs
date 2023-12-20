using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webex.Data;
using webex.Models;

namespace webex.Pages.Workouts
{
    public class EditModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;
        public List<Models.Exercise> Exercises { get; set; } = new();
        [BindProperty]
        public Workout Workout { get; set; } = default!;
        [BindProperty]
        public WorkoutExercise WorkoutExercise { get; set; } = new();

        public EditModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }

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
            Workout = workout;
            return Page();
        }
        
        public async Task<IActionResult> OnPostExercise(int? id)
        {
            var workout = await getWorkoutMapped(id);

            var newEx = new WorkoutExercise();
            
            if(workout == null){
                workout = Workout;
            }

            if(workout.WorkoutExercises == null){
                workout.WorkoutExercises = new List<WorkoutExercise>();
            }

            workout.WorkoutExercises.Add(newEx);
            await _context.SaveChangesAsync();

            return Redirect("/Workouts/Edit?id=" + id);
        }

        public async Task<IActionResult> OnPostExerciseUpdate(int? workoutId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(WorkoutExercise.Exercise?.Id != null)
            {
                WorkoutExercise.Exercise = await _context.Exercises
                    .Where(e => e.Id == WorkoutExercise.Exercise.Id)
                    .FirstOrDefaultAsync();    
            }

            var workout =  await getWorkoutMapped(workoutId);             
            var oldIndex = workout.WorkoutExercises.FindIndex(we => we.Id == WorkoutExercise.Id);
            workout.WorkoutExercises[oldIndex] = WorkoutExercise;
            
            await _context.SaveChangesAsync();

            return Redirect("/Workouts/Edit?id=" + workoutId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(Workout.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkoutExists(int id)
        {
          return (_context.Workouts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<Workout> getWorkoutMapped(int? id)
        {
            var wo = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if(wo == null) throw new Exception("Enity not found"); 
            
            return wo;
        }

    }
}
