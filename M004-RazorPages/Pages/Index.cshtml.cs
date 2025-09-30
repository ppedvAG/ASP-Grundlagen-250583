using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

/// <summary>
/// Dependency Injection wie bei Controller (über Konstruktor)
/// 
/// Generell hängen bei Razor Pages das Frontend & Backend zusammen
/// Über @model [Klassenname] im Frontend
/// </summary>
public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
	public string User { get; private set; }

	/// <summary>
	/// OnGet
	/// 
	/// Äquivalent zu einer Controller Methode mit einem return View() am Ende
	/// Wird nur bei Get-Requests aufgerufen
	/// Kann auch ein IActionResult zurückgeben (statt void)
	/// </summary>
	public void OnGet(string Username)
	{
		User = Username; //Werte die im HTML angegriffen werden sollen, müssen hier als Variablen angelegt werden
	}
}
