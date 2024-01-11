using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webex.Models;

namespace webex.Data
{
    public class DbContextEx : IdentityDbContext<IdentityUser>
    {
        public DbContextEx(DbContextOptions<DbContextEx> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MuscleGroup>().Property(e => e.Location).HasConversion<string>();
            builder.Entity<MuscleGroup>().Property(e => e.MuscleFunction).HasConversion<string>();

            builder.Entity<Workout>().HasQueryFilter(wa => wa.User.Id  == GetCurrentUserId());
        }

        private string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            return userId;
        }

        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    }
}