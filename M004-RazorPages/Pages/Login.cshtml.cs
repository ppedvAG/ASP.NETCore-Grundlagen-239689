using M004_RazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

public class LoginModel : PageModel
{
	private List<User> users;

	public LoginModel(List<User> users) => this.users = users;

	public IActionResult OnPost(string username, string passwort)
	{
		if (users.Any(e => e.Username == username) && users.First(e => e.Username == username).Passwort == passwort)
		{
			//Anonymes Objekt: Wird benötigt um Parameter bei Razor Pages weiterzugeben
			//Die Parameternamen bestimmen wie diese auf der anderen Seite heißen müssen
			return RedirectToPage("Index", new { user = username, pw = passwort });
		}
		else
		{
			return Forbid();
		}
	}
}
