using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace M003_MVC.Controllers;

public class LoginController : Controller
{
	//Liste von Usern per DI einbinden
	private List<User> users;

	private ILogger<LoginController> logger;

    public LoginController(ILogger<LoginController> logger, List<User> users)
    {
		this.users = users;
		this.logger = logger;
    }

    public IActionResult Index()
	{
		logger.Log(LogLevel.Information, "Login geöffnet {0}", HttpContext.Connection.RemoteIpAddress);
		return View();
	}

	public IActionResult Login()
	{
		return View();
	}

	public IActionResult Registrieren()
	{
		return View();
	}

	/// <summary>
	/// Über asp-for="..." werden hier die Parameter verfügbar
	/// </summary>
	public IActionResult Einloggen(string Username, string Passwort)
	{
		if (users.Any(e => e.Username == Username) && users.First(e => e.Username == Username).Passwort == Passwort)
		{
			return View("Index", new LoginModel() { Username = Username, Passwort = Passwort });
		}
		else
		{
			return Forbid();
		}
	}

	public IActionResult NeuerUser(string Email, string Username, string Passwort)
	{
		users.Add(new User() { Email = Email, Username = Username, Passwort = Passwort });
		return View("Index");
	}
}
