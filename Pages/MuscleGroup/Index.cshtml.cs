using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webex.Models;

namespace webex.Pages_MuscleGroup
{
    public class IndexModel : PageModel
    {
        private readonly Db_Context _context;

        public IndexModel(Db_Context context)
        {
            _context = context;
        }

        public IList<MuscleGroup> MuscleGroup { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MuscleGroups != null)
            {
                MuscleGroup = await _context.MuscleGroups.ToListAsync();
            }
        }
    }
}
