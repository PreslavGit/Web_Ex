using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webex.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<DbContextEx>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db_ContextEx") ?? throw new InvalidOperationException("Connection string 'Db_ContextEx' not found.")));
builder.Services.AddDbContext<DbContextEx>(options => {
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connString);
});

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DbContextEx>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DbContextEx>();
    
    Seeds.Initialize(context);

    System.Console.WriteLine(context.Database.CanConnect() ? 
        "@@@ Successfully connected to database" : 
        "@@@ Error connection to database"
    );
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
