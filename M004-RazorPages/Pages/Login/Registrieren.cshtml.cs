using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Login;

public class RegistrierenModel(List<User> users) : PageModel
{
	/// <summary>
	/// Die OnGet() Methode(n) k�nnen auch IActionResult zur�ckgeben
	/// Hier wird statt return View() return Page() verwendet
	/// 
	/// WICHTIG: �ber die Page()-Methode k�nnen keine Daten ans Frontend weitergegeben werden
	/// Stattdessen werden innerhalb dieser Klasse Variablen angelegt, diese werden im Frontend angegriffen
	/// </summary>
	public IActionResult OnGet()
	{
		return Page(); //return Page() == return View()
	}

	/// <summary>
	/// WICHTIG: Jede Handler Methode muss mit OnGet... oder OnPost... definiert werden
	/// 
	/// Wenn �ber asp-page-handler diese Methode ausgef�hrt werden soll, MUSS diese mit OnPost... definiert werden
	/// </summary>
	public IActionResult OnPostRegisterButton(string user, string pw)
	{
		if (users.Any(u => u.Username == user))
			return BadRequest();

		//Evtl. weitere Anforderungen (z.B. Username zw. 3 und 15 Zeichen, Passwort mit allen m�glichen Anforderungen, ...)

		//Neuen User anlegen und in Liste hinzuf�gen
		User newUser = new()
		{
			Username = user,
			Password = pw
		};

		users.Add(newUser);

		//User zum Login weiterleiten
		return RedirectToPage("Login"); //WICHTIG: Statt RedirectToAction hier RedirectToPage verwenden
	}
}