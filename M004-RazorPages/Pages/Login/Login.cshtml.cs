using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Login;

public class LoginModel(List<User> users) : PageModel
{
	public void OnGet() { }

	public IActionResult OnPostLoginButton(string user, string pw)
	{
		//Suche den User aus der Liste
		User? foundUser = users.FirstOrDefault(u => u.Username == user);
		if (foundUser == null)
			return BadRequest();

		if (foundUser.Password != pw)
			return Forbid();

		//Username gefunden, Passwort korrekt
		return RedirectToPage("Index", foundUser);
	}
}
