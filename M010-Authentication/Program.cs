using M_011_Authentication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//IdentityUser: Beschreibt, die default User Klasse mit Email, Passwort, TelNr, ...
//Kann vererbt werden, um eigene Felder hinzuzuf�gen
//Wird auch verwendet, um eine eigene DB f�r die Userdaten zu verwenden
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

//Wo sind die Login/Registrierungspages?
//Alle Pages die sich um Login drehen, befinden sich einen externen Library
//Es können einzelne Pages erstellt werden über:
//Rechtsklick -> Add -> New Scaffolded Item -> Identity
//Entsprechende Pages auswählen


//User verwenden
//Drei Dinge: HttpContext, SignInManager, UserManager

//HttpContext
//Enthält alle Informationen zum derzeitigen Client, Request, Response, User, Connection, ...

//SignInManager
//Ermöglicht, allgemeine Userverwaltung -> Login/Logout, 2FA, ...

//UserManager
//Gibt spezifische Informationen über den derzeitigen User

//Die Manager werden über DI geliefert

//Usermanagement Einstellungen
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;

	options.User.RequireUniqueEmail = true;
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

	options.Lockout.MaxFailedAccessAttempts = 5;
});

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();