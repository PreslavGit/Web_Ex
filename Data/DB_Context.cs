using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class Db_Context : IdentityDbContext<IdentityUser>
{
    public Db_Context(DbContextOptions<Db_Context> options)
        : base(options)
    {
    }

    // public DbSet<Model> Model { get; set; }
}