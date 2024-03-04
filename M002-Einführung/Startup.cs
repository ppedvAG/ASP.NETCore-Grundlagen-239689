using Microsoft.AspNetCore.Builder;

namespace M002_Einführung;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<DITest>(); //Ein Objekt pro Lifecycle der Applikation (genau eines)
		services.AddScoped<DITest>(); //Ein Objekt pro "Page" pro User
		services.AddTransient<DITest>();
	}

	public void ConfigureMiddleware(WebApplication app, IWebHostEnvironment env)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage(); //Zeigt eine große Info an, falls eine Exception aufgetreten ist
			app.UseStatusCodePages(); //Zeigt eine kurze Info zum StatusCode an, falls ein Fehler auftritt
		}

		app.UseStaticFiles(); //Middleware für statisches HTML, CSS und JS
		app.UseRouting(); //Routing aktivieren, notwendig für Map Methoden


		//Map
		//Erzeugt Routen zur Navigation der Pages
		//MapGet, MapPost, MapDelete, Put, Patch, ...
		app.MapGet("/", () => "Hello World!");

		app.MapGet("/Hello", Hello);
		//Delegate: Methodenzeiger
		//Kurzform: Lambda Expressions (...) =>
		//Können Parameter haben, diese werden in den Klammern angegeben

		app.MapGet("/xyz", () => "<h1>Willkommen bei xyz</h1>");

		app.MapPost("/", () => "Hello World!");
		app.MapPost("/key", (string key) => $"Dein Key ist: {key}"); //Anonyme Form (mit einem Parameter)
		app.MapPost("/key", Key); //Mit eigener Methode
	}

	string Hello()
	{
		return "Hello World!";
	}

	string Key(string key)
	{
		return $"Dein Key ist: {key}";
	}

	bool IsAdmin(HttpContext context)
	{
		return context.User.IsInRole("Admin");
	}
}