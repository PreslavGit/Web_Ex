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
    public class IndexModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;

        public IndexModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }

        public IList<Exercise> Exercise { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Exercises != null)
            {
                Exercise = await _context.Exercises.ToListAsync();
            }
        }
    }
}
