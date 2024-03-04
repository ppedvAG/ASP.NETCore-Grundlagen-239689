using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M003_MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		//Per DI kommt der Logger in den Controller
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		//Jede Methode in einem Controller stimmt generell mit einer View überein
		//IActionResult
		//Resultat von einem HTTP Request
		//Hat generell einen StatusCode dahinter
		//View = 200 OK
		public IActionResult Index()
		{
			//return Redirect("https://ppedv.de/"); //Umleitung zu anderen Webseiten möglich
			//return RedirectToAction("Privacy", "Home"); //Zwischen Seiten innerhalb von Controllern springen

			//return NotFound(); //404
			//return BadRequest(); //400
			//return Forbid(); //403

			//return StatusCode(403); //StatusCode per Parameter übergeben (selbiges wie Forbid())

			return View();
		}

		//View: Zeigt die View hinter der Methode und dem Controller an
		//Privacy(): /Home/Privacy -> Privacy.cshtml File
		public IActionResult Privacy()
		{
			//return Redirect("https://ppedv.de/");
			return View();
		}

		//Hier wird eine Methode angelegt, die per asp-action verwendet werden kann
		//Rechtsklick -> Add View
		public IActionResult Hallo()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
