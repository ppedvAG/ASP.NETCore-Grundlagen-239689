using M006_Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class BestellungBearbeitenModel : PageModel
{
	private readonly NorthwindContext db;

	public BestellungBearbeitenModel(NorthwindContext db) => this.db = db;

	[BindProperty]
	public Order o { get; set; }

	public void OnGet(int OrderId)
	{
		o = db.Orders.First(e => e.OrderId == OrderId);
	}

	public IActionResult OnPost(Order o)
	{
		if (!ModelState.IsValid)
			return BadRequest();

		if (db.Orders.Any(e => e.OrderId == o.OrderId))
			return BadRequest();

		db.Orders.Update(o);
		db.SaveChanges();
		return RedirectToPage("/Index");
	}
}