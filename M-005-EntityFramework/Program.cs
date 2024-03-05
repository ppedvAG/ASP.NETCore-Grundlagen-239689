using M_005_EntityFramework.Models.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DB Context per DI anmelden
//builder.Services.AddDbContext<NorthwindContext>(); //ConnectionString in DbContext

//Der ConnectionString sollte nicht in der Context Klasse stehen, sondern in einer Config (appsettings.json)
builder.Services.AddDbContext<NorthwindContext>(
	e => e.UseSqlServer(builder.Configuration.GetConnectionString("Northwind"))); //Hier wird der ConnectionString per Lambda-Expression aus der Config entnommen

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
