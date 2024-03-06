using M006_Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class BestellungenModel : PageModel
{
	private readonly NorthwindContext db;

	public BestellungenModel(NorthwindContext db) => this.db = db;

	[BindProperty] //.../BestellungErstellen?o=...
	public Order o { get; set; } = new();

	public void OnGet() { }

	[HttpPost]
	public IActionResult OnPost()
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		if (db.Orders.Any(e => e.OrderId == o.OrderId))
			return BadRequest(o.OrderId);

		db.Orders.Add(o);
		db.SaveChanges();
		return RedirectToPage("Index");
	}
}