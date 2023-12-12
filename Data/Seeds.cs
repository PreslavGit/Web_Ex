using Microsoft.EntityFrameworkCore;
using webex.Data;

public static class Seeds
{
    public static void Initialize(DbContextEx context)
    {
        context.Database.Migrate();

        // var seedData = new List<Model>
        // {
        //     new Model{},
        // };

        // foreach (var item in seedData)
        // {
        //     context.table.Add(item);
        // }

        // context.SaveChanges();
    }
}
