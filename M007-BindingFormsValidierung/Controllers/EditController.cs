using Microsoft.AspNetCore.Mvc;

namespace M007_BindingFormsValidierung.Controllers;

public class EditController : Controller
{
	[BindProperty(SupportsGet = true)]
	public string customerID { get; set; }

	public IActionResult Index()
	{
		return View("Index", customerID);
	}

	public IActionResult Save()
	{
		//Save to DB
		return RedirectToAction("Index", "Home");
	}
}
