using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

/// <summary>
/// Bei Razor Pages werden Controller und Model vereint
/// Die Model Klasse ist gleichzeitig die Controller Klasse
/// </summary>
public class IndexModel : PageModel
{
	private readonly ILogger<IndexModel> _logger;

	public IndexModel(ILogger<IndexModel> logger) => _logger = logger;

	//public IActionResult OnGet()
	//{
	//	return Page(); //return Page() == return View()
	//}

	/// <summary>
	/// Hier müssen die Parameter so genannt werden, wie sie im anonymen Objekt heißen
	/// </summary>
	public IActionResult OnGet(string user, string pw)
	{
		//ViewData: Beliebige Dinge in einem Dictionary ablegen, um diese in der View angreifen zu können
		ViewData["user"] = user;
		ViewData["pw"] = pw;
		return Page();
	}

	public IActionResult OnPost()
	{
		return Page();
	}
}
