using M006_Data.Models;
using M007_BindingFormsValidierung.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace M007_BindingFormsValidierung.Controllers
{
	public class HomeController : Controller
	{
		//Wenn ein Request hinein kommt mit dem Query-Parameter Test, soll dieser hier gefangen werden
		[BindProperty(SupportsGet = true)] //Wenn jetzt bei einer beliebigen URL auf den HomeController ein HttpParameter namens "Test" mitgegeben wird, wird dieser hier gefangen
		public string Test { get; set; }

		private readonly ILogger<HomeController> _logger;

		private readonly NorthwindContext db;

		public HomeController(ILogger<HomeController> logger, NorthwindContext db)
		{
			this.db = db;
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		//[ActionName("Privacy")] //Benennt die Methode für die GUI (asp-action) um
		public async Task<IActionResult> ShowAllCustomers(string country)
		{
			if (country != null)
				return View(await db.Customers.Where(e => e.Country == country).ToListAsync());
			return View(await db.Customers.ToListAsync());
		}

		/// <summary>
		/// Wenn der User bearbeiten bei einem Customer klickt, soll sich eine neue View öffnen, und die Daten von dem Customer sollen automatisch eingefüllt werden
		/// </summary>
		public IActionResult CustomerBearbeiten(string customerID)
		{
			return RedirectToAction("Index", "Edit", new { customerID = customerID });
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
