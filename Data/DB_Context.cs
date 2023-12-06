using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webex.Models;

public class Db_Context : IdentityDbContext<IdentityUser>
{
    public Db_Context(DbContextOptions<Db_Context> options)
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
}