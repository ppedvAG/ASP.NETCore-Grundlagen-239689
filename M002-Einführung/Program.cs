using M002_Einführung;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Dependency Injection
//Objekte in Pages inkludieren
//z.B. Datenbankverbindungen

builder.Services.AddSingleton<DITest>(); //Ein Objekt pro Lifecycle der Applikation (genau eines)
builder.Services.AddScoped<DITest>(); //Ein Objekt pro "Page" pro User
builder.Services.AddTransient<DITest>(); //Ein Objekt pro Request

//Objekte werden hier nur registriert und im Konstruktor der entsprechenden Page empfangen
//Es müssen nicht alle Objekte empfangen werden

WebApplication app = builder.Build();

//Middleware
//Code, der bei Requests/Responses ausgeführt wird
//Use und Map Methoden
//Use: Aktiviert Middleware
//Map: Routing

//Wenn es sich um eine Dev-Umgebung handelt, füge die DevException Page hinzu
//Um welche Umgebung es sich handelt, wird in launchSettings.json festgelegt
//Development, Staging, Production, eigene Umgebung

//app.Environment.IsEnvironment("Test"); //Eigene Umgebung namens Test
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


//MapWhen, UseWhen
//Map und Use mit Bedingungen
//z.B. Wenn der User ein Admin ist
//app.MapWhen((context) => context.User.IsInRole("Admin"), (app) => app.Map("/admin"));
//app.MapWhen(IsAdmin, (app) => app.Map("/admin"));

app.Run();

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