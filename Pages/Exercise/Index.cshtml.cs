using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webex.Data;
using webex.Migrations;
using webex.Models;

namespace webex.Pages_Exercise
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
        public PaginatedList<Exercise> Exercise { get; set; } = default!;

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

            IQueryable<Exercise> exercisesIQ = from ex in _context.Exercises
                                               select ex;

            if (!String.IsNullOrEmpty(searchString))
            {
                exercisesIQ = exercisesIQ.Where(ex =>
                ex.Name.Contains(searchString)
               || ex.MuscleGroups.Any(m => m.Name.Contains(searchString))
                );
            }

            switch (sortOrder)
            {
                case "Name_desc":
                    exercisesIQ = exercisesIQ.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    exercisesIQ = exercisesIQ.OrderBy(s => s.Name);
                    break;
                
            }
            if (_context.Exercises != null)
            {
                int pageSize = 5;
                Exercise = await PaginatedList<Exercise>.CreateAsync(
                    exercisesIQ.Include(ex => ex.MuscleGroups).AsNoTracking(), pageIndex ?? 1, pageSize);
            }
        }
    }
}
