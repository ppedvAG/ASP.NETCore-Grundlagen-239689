using M_005_EntityFramework.Models;
using M_005_EntityFramework.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Diagnostics;

namespace M_005_EntityFramework.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> logger;

		private readonly NorthwindContext db;

		public HomeController(ILogger<HomeController> logger, NorthwindContext db)
		{
			this.logger = logger;
			this.db = db;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult PlainSQLConnection()
		{
			//Basic SQL-Connection
			//Benötigt einen DB-Treiber

			//Connection String: String, welcher Informationen für die Verbindung enthält
			//u.A.: Hostname, Username, Passwort, DB, Verschlüsselung, ...
			SqlConnection connection =
				new SqlConnection("Data Source=WIN10-LK3;Initial Catalog=Northwind;Integrated Security=True;Encrypt=False");
			connection.Open();

			//Command: Container für ein SQL-Statement
			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM Customers"; //Beliebiges SQL-Statement

			//ExecuteReader: Datensätze lesen
			//ExecuteScalar: Einzelnen Wert (z.B. Average, Sum, ...)
			//ExecuteNonQuery: Einzelner int (z.B. Count, Ergebnis einer Stored Procedure, ...)
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read()) //Solange es weitere Rows gibt, lies diese ein
			{
				object[] datensatz = new object[reader.VisibleFieldCount]; //Array erstellen mit Länge = Anzahl Spalten
				reader.GetValues(datensatz); //Lies den derzeitigen Datensatz
				//object[] ist generisch -> Wenn ein Datumswert heraus kommen soll, muss dieser erstmal konvertiert werden
				
				//Customers:
				//class Customer
				//{ public string CustomerID, public string CompanyName, ... }

				//Jedes object[] wird zu einem Customer Objekt konvertiert
				//Jedes Customer Objekt stellt einen Datensatz dar
				//-> List<Customer>
			}

			return View();
		}

		public IActionResult EFCore()
		{
			//Entity Framework
			//Automatisiert die Verbindung und das Mapping zu den Objekten

			//Requirements (NuGet):
			//Microsoft.EntityFrameworkCore
			//Microsoft.EntityFrameworkCore.SqlServer
			//Microsoft.EntityFrameworkCore.Design
			//Microsoft.EntityFrameworkCore.Tools

			//Instanzierung von EF Core
			//Per Konsole mit dotnet ef
			//Per EFCore Power Tools

			//Rechtsklick aufs Projekt -> EFCore Power Tools -> Reverse Engineer
			//Generiert jetzt die Model Klassen und die Context Klasse
			//Context Klasse: Stellt die Datenverbindung dar, wird per DependencyInjection in die Pages gefüllt

			//Die Context Klasse enthält eine Auflistung von DbSets, jedes DbSet korreliert mit einer Tabelle
			//Die DbSets werden per Linq angesprochen, diese Linq Queries werden zu SQL übersetzt, und auf der DB ausgeführt
			//Nach der Ausführung kommen normale C# Objekte zurück

			List<Customer> ukCustomers = db.Customers.Where(e => e.Country == "UK").ToList(); //SELECT * FROM Customers WHERE Country = "UK"

			return View(ukCustomers);
		}

		public async Task<IActionResult> VieleDaten()
		{
			//Problemstellung: Hier werden 1M+ Daten geladen
			//-> Blockieren des WebServers
			//Wenn viele Daten geladen werden, werden andere Clients auch blockiert
			//Lösung: async/await

			//3 Schritte:
			//Prozess starten (Daten laden)
			//Zwischenschritte (User informieren)
			//Auf das Ergebnis warten (Warten auf das Laden der Daten mit await)
			Task<List<KundenUmsatz>> vieleDatenAufgabe = db.KundenUmsatzs.ToListAsync(); //Prozess starten (Datentyp mit Task umgeben)
			//Zwischenschritte: Kleines Update									 
			List<KundenUmsatz> vieleDaten = await vieleDatenAufgabe; //Auf Ergebnis warten

			//Die Methode muss selbst async sein, die Methode muss einen Task-Typ zurückgeben

			return View(vieleDaten);
		}

		public IActionResult Join()
		{
			//Orders und Customers über CustomerID joinen
			List<Tuple<Order, Customer>> list = db.Orders.Join
			(
				db.Customers, //Die andere Tabelle
				o => o.CustomerId, //Schlüssel Tabelle 1 (Orders)
				c => c.CustomerId, //Schlüssel Tabelle 2 (Customers)
				(o, c) => new Tuple<Order, Customer>(o, c)
				//Lambda-Expression mit zwei Parametern: (o, c): Tabelle 1 und 2
				//Nach => das Resultat, hier ein Tupel
			).ToList();

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
