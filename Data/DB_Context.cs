using Microsoft.EntityFrameworkCore;

public class Db_Context : DbContext
{
    public Db_Context(DbContextOptions<Db_Context> options)
        : base(options)
    {
    }

    // public DbSet<Model> Model { get; set; }
}