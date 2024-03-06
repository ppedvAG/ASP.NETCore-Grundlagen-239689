using M006_Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace M007_BindingFormsValidierung.Controllers;

public class EditController : Controller
{
	[BindProperty(SupportsGet = true)]
	public string customerID { get; set; }

	private readonly NorthwindContext db;

	public EditController(NorthwindContext db)
	{
		this.db = db;
	}

	public IActionResult Index()
	{
		Customer c = db.Customers.First(e => e.CustomerId == customerID);
		return View("Index", c);
	}

	/// <summary>
	/// Der Customer Parameter kommt vom Model im Frontend
	/// </summary>
	public IActionResult Save(Customer customer)
	{
		//Save to DB
		if (!ModelState.IsValid) //Sind die Daten noch konsistent zwischen View/Backend und DB?
		{
			return StatusCode(500);
		}

		if (db.Customers.Any(e => e.CustomerId == customer.CustomerId)) //Wenn die CustomerID geändert wird, kann EF kein Update machen
		{
			db.Update(customer);
			db.SaveChanges();
		}
		else
		{
			//db.Add(customer);
			return NotFound();
		}

		return RedirectToAction("Index", "Home");
	}
}
