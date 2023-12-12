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
    public class IndexModel : PageModel
    {
        private readonly DbContextEx _context;

        public IndexModel(DbContextEx context)
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
