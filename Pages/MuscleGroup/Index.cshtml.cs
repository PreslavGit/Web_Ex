using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webex.Models;
using webex.Data;
using webex.Migrations;

namespace webex.Pages_MuscleGroup
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
        private readonly DbContextEx _context;

        public IndexModel(DbContextEx context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string LocationSort { get; set; }
        public string MuscleFunctionSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
        public PaginatedList<MuscleGroup> MuscleGroup { get; set; } = default!;
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentFilter = searchString;

            NameSort = string.IsNullOrEmpty(sortOrder) ? "Name" : "";
            LocationSort = sortOrder == "Location" ? "Location_desc" : "Location";
            MuscleFunctionSort = sortOrder == "MuscleFunction" ? "MuscleFunction_desc" : "MuscleFunction";

            if(searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentSort = sortOrder;
            CurrentFilter = searchString;

            IQueryable<MuscleGroup> muscleGroupsIQ = from m in _context.MuscleGroups
                                                     select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                muscleGroupsIQ = muscleGroupsIQ.Where(m =>
                m.Name.Contains(searchString)
                );
            }


            switch (sortOrder)
            {
                case "Name_desc":
                    muscleGroupsIQ = muscleGroupsIQ.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    muscleGroupsIQ = muscleGroupsIQ.OrderBy(s => s.Name);
                    break;
                case "Location_desc":
                    muscleGroupsIQ = muscleGroupsIQ.OrderByDescending(s => s.Location);
                    break;
                case "Location":
                    muscleGroupsIQ = muscleGroupsIQ.OrderBy(s => s.Location);
                    break;
                case "MuscleFunction_desc":
                    muscleGroupsIQ = muscleGroupsIQ.OrderByDescending(s => s.MuscleFunction);
                    break;
                case "MuscleFunction":
                    muscleGroupsIQ = muscleGroupsIQ.OrderBy(s => s.MuscleFunction);
                    break;
            }

            if (_context.MuscleGroups != null)
            {
                //  MuscleGroup = await _context.MuscleGroups.ToListAsync();
                int pageSize = 5;
                MuscleGroup = await PaginatedList<MuscleGroup>.CreateAsync(
                    muscleGroupsIQ.AsNoTracking(), pageIndex ?? 1, pageSize
                );
            }

        }

    }
}
