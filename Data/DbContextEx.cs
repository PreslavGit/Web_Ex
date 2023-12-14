using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webex.Models;

namespace webex.Data
{
    public class DbContextEx : IdentityDbContext<IdentityUser>
    {
        public DbContextEx(DbContextOptions<DbContextEx> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MuscleGroup>().Property(e => e.Location).HasConversion<string>();
            builder.Entity<MuscleGroup>().Property(e => e.MuscleFunction).HasConversion<string>();
        }

        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    }
}