using M_011_Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;

namespace M_011_Authentication.Controllers
{
	[AllowAnonymous] //Alle User (auch ohne Anmeldung) können hier zugreifen
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> logger;
		private readonly UserManager<IdentityUser> um;
		private readonly SignInManager<IdentityUser> sim;

		public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
		{
			this.logger = logger;
			this.um = um;
			this.sim = sim;
		}

		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Das geheime Admin Portal soll nur von Admins angreifbar sein
		/// </summary>
		//[Authorize(Roles = "Admin")] //Das Authorize Attribut leitet zur Login-Page weiter
		public IActionResult Privacy()
		{
			if (HttpContext.User.IsInRole("Admin")) //Access denied Page
				return View();
			else
				return Forbid();
		}

		[HttpGet("/createadmin")]
		public async Task<IActionResult> CreateAdmin()
		{
			IdentityUser user = new IdentityUser() { Email = "Test@ppedv.de", PhoneNumber = "0123456789", UserName = "Lukas" };

			await um.DeleteAsync(user);
			if (await um.FindByEmailAsync(user.Email) == null) //Wenn der User nicht existiert, wird er erstellt
				await um.CreateAsync(user, "123456");
			await um.AddToRoleAsync(user, "Admin");

			await sim.SignInAsync(user, true);

			return Ok();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
