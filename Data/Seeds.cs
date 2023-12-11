using Microsoft.EntityFrameworkCore;

public static class Seeds
{
    public static void Initialize(Db_Context context)
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
