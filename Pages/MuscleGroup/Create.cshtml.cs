using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webex.Models;

namespace webex.Pages_MuscleGroup
{
    public class CreateModel : PageModel
    {
        private readonly Db_Context _context;

        public CreateModel(Db_Context context)
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
