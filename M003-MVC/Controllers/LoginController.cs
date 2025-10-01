using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace M003_MVC.Controllers;

/// <summary>
/// Simples Loginportal
/// 
/// User soll sich registrieren und anmelden können
/// Hier werden drei Methoden benötigt: Index, Anmelden, Registrieren
/// 
/// Technische Umsetzung:
/// - Globale Userliste (Dependency Injection)
/// - HTML für Anmeldung/Registrierung (HTML-Form)
/// - C# Code für Anmeldung/Registrierung (Gibt es den User, stimmen Username & Passwort, ...)
/// </summary>
public class LoginController(List<User> users) : Controller //Primary Constructor: Legt automatisch private Felder an
{
	////Wird per DI empfangen
	//private readonly List<User> _users;

	//public LoginController => _users = users;

	[HttpGet]
	public IActionResult Index()
	{
		string? cookie = HttpContext.Request.Cookies["AngemeldetBleiben"];
		if (cookie != null)
		{
			string[] data = cookie.Split(",");
			User loggedIn = new User
			{
				Username = data[0],
				Password = data[1]
			};
			return View(loggedIn);
		}

		return View();
	}

	/// <summary>
	/// Rechtsklick auf Methodenname -> Add View
	/// 
	/// IActionResult: Antwort des Requests (Response)
	/// Gibt einen beliebigen HTTP-Statuscode zurück
	/// </summary>
	[HttpGet]
	public IActionResult Login()
	{
		//return BadRequest(); //400
		//return Forbid(); //403
		//return Ok(); //200
		//return RedirectToAction("Index", "Home"); //302
		//return StatusCode(123); //Beliebiger StatusCode

		return View(); //View: Dazugehörige .cshtml-Datei (Code 200)
	}

	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	public IActionResult RegisterButton(string user, string pw)
	{
		//bool userExists = false;
		//foreach (User u in users)
		//	if (u.Username == user)
		//		userExists = true;

		//if (userExists)
		//	return BadRequest();

		//Gibt es den User bereits?
		if (users.Any(u => u.Username == user))
			return BadRequest();

		//Evtl. weitere Anforderungen (z.B. Username zw. 3 und 15 Zeichen, Passwort mit allen möglichen Anforderungen, ...)

		//Neuen User anlegen und in Liste hinzufügen
		User newUser = new()
		{
			Username = user,
			Password = pw
		};

		users.Add(newUser);

		//User zum Login weiterleiten
		return RedirectToAction("Login");
	}

	public IActionResult LoginButton(string user, string pw, string ab)
	{
		//Suche den User aus der Liste
		User? foundUser = users.FirstOrDefault(u => u.Username == user);
		if (foundUser == null)
			return BadRequest();

		if (foundUser.Password != pw)
			return Forbid();

		if (ab == "on")
			HttpContext.Response.Cookies.Append("AngemeldetBleiben", $"{foundUser.Username},{foundUser.Password}");

		//Username gefunden, Passwort korrekt
		return View("Index", foundUser);
	}
}