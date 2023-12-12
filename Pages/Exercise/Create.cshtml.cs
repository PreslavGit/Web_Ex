using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webex.Data;
using webex.Models;

namespace webex.Pages_Exercise
{
    public class CreateModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;
        public List<MuscleGroup> MuscleGroups { get; set; }
        [BindProperty]
        public List<int> SelectedMuscleGroupIds { get; set; }
        [BindProperty]
        public Exercise Exercise { get; set; } = default!;

        public CreateModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            MuscleGroups = await _context.MuscleGroups.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Exercises == null || Exercise == null)
            {
                return await OnGetAsync();
            }
            foreach (var id in SelectedMuscleGroupIds){
                System.Console.WriteLine(id);
            }
            return await OnGetAsync();
            _context.Exercises.Add(Exercise);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
