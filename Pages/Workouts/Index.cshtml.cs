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
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage { get { return (PageIndex > 1); } }
        public bool HasNextPage { get { return (PageIndex < TotalPages); } }
        public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
            (pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
    public class IndexModel : PageModel
    {
        private readonly webex.Data.DbContextEx _context;

        public IndexModel(webex.Data.DbContextEx context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
        [BindProperty]
        public PaginatedList<Models.Workout> Workout { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentFilter = searchString;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "Name" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentSort = sortOrder;
            CurrentFilter = searchString;

            IQueryable<Workout> workoutsIQ = from wo in _context.Workouts
                                                    select wo;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                workoutsIQ = workoutsIQ.Where(ex =>
                ex.Name.Contains(searchString)
               || ex.WorkoutExercises.Any(m => m.Exercise.Name.Contains(searchString))
                );
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    workoutsIQ = workoutsIQ.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    workoutsIQ = workoutsIQ.OrderBy(s => s.Name);
                    break;
                
            }

            if (_context.Workouts != null)
            {
                int pageSize = 5;
                Workout = await PaginatedList<Workout>.CreateAsync(
                workoutsIQ.Include(w => w.WorkoutExercises)
                    .ThenInclude(ex => ex.Exercise)
                .AsNoTracking(), pageIndex ?? 1, pageSize);
            }
        }

    }
}
