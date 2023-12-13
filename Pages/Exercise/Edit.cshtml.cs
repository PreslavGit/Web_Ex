using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using webex.Models;


namespace webex.Pages_Exercise
{
    public class EditModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;
        public List<MuscleGroup> MuscleGroups { get; set; }
        public List<int> SelectedMuscleGroups = new List<int>();
        [BindProperty]
        public List<int> SelectedMuscleGroupIds { get; set; }
        public EditModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }

        [BindProperty]
        public Exercise Exercise { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Exercises == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.MuscleGroups)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            Exercise = exercise;
            MuscleGroups = await _context.MuscleGroups.ToListAsync();
            this.MapChecked();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Exercise).State = EntityState.Modified;

            try
            {
                // _context.Update(Exercise);

                MuscleGroups = await _context.MuscleGroups.ToListAsync();
                this.AppendMuscleGroups();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(Exercise.Id))
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

        private bool ExerciseExists(int id)
        {
            return (_context.Exercises?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void MapChecked()
        {

            foreach (var emg in Exercise.MuscleGroups)
            {
                foreach (var mg in MuscleGroups)
                {
                    if (emg.Id == mg.Id)
                    {
                        SelectedMuscleGroups.Add(mg.Id);
                    }
                }
            }
        }

        private void AppendMuscleGroups()
        {
            this.deleteOldMuscleGroups();

            foreach (var muscleGroupId in SelectedMuscleGroupIds)
            {
                var muscleGroupEntity = MuscleGroups.Find(mg => mg.Id == muscleGroupId);
                if (muscleGroupEntity != null)
                {
                    Exercise.MuscleGroups.Add(muscleGroupEntity);
                }
            }
        }

        private void deleteOldMuscleGroups(){
            var muscleGroupsToDelete = _context.MuscleGroups
                .Where(mg => mg.Exercises.Any(e => e.Id == Exercise.Id))
                .Include(mg => mg.Exercises)
                .ToList();

            foreach (var muscleGroup in muscleGroupsToDelete)
            {   
                var removedCnt = muscleGroup.Exercises.RemoveAll(e => e.Id == Exercise.Id);
            }
            _context.SaveChanges();
        }
    }
}
