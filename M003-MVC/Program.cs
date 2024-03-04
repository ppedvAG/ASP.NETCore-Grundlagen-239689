using M003_MVC.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<List<User>>();

WebApplication app = builder.Build();

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

//Authentication vs. Authorization
//Authentication: Gibt dem User die Möglichkeit, sich anzumelden
//Authorization: Sagt dem User, was er darf
app.UseAuthentication();
app.UseAuthorization();

//Middlewarekomponenten haben eine fixe Reihenfolge, bzw. Reihenfolge muss beachtet werden

//MapControllerRoute:
//Definiert das Mapping von einem Controller
//pattern gibt das Muster vor
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

//Aufgabenstellung: Eigener Controller, welcher ein Loginportal simuliert

app.Run();