using M004_RazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

public class RegistrierenModel : PageModel
{
	private List<User> users;

	public RegistrierenModel(List<User> users) => this.users = users;

	public IActionResult OnPost(string email, string username, string passwort)
	{
		users.Add(new User() { Username = username, Passwort = passwort });
		return RedirectToPage("Index"); //RedirectToPage(...) statt View(...)
	}
}