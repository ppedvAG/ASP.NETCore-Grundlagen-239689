using M_006_EFListen.Models;
using M006_Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace M_006_EFListen.Controllers
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

		public IActionResult Index() => View();

		public IActionResult Privacy() => View();

		//Alle Kunden anhand des Landes filtern
		[HttpGet]
		public async Task<IActionResult> ShowAllCustomers(string country)
		{
			if (country != null)
				return View(await db.Customers.Where(e => e.Country == country).ToListAsync());
			return View(await db.Customers.ToListAsync());
		}

		/// <summary>
		/// CustomerID kommt von der GUI mittels asp-route-customerID
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> ShowOrdersFor(string customerID)
		{
			List<Order> bestellungen = await db.Orders.Where(e => e.CustomerId == customerID).ToListAsync();
			return View(bestellungen);
		}

		/// <summary>
		/// Diese Methode soll ein beliebige Liste nehmen können, und ihren Inhalt darstellen
		/// </summary>
		public async Task<IActionResult> ShowAnyData()
		{
			//Interface für alle Listentypen
			IEnumerable<object> list = await db.Orders.ToListAsync();
			return View(list);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
