using M008_Lokalisierung.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace M008_Lokalisierung.Controllers;

//Generic beim IStringLocalizer hängt mit den Dateinamen zusammen
public class HomeController(IStringLocalizer<HomeController> loc) : Controller
{
	public IActionResult Index()
	{
		LocalizedString str = loc["Test"]; //LocalizedString: Wrapper für den Text; wenn die Sprache geändert wird, ist hier immer der richtige Text enthalten
		return View("Index", str.Value);
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	[HttpPost]
	public IActionResult SetLanguage(string culture, string returnUrl)
	{
		Response.Cookies.Append(
			CookieRequestCultureProvider.DefaultCookieName,
			CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
			new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
		);

		return LocalRedirect(returnUrl);
	}
}
