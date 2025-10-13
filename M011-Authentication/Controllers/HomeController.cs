using M011_Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Principal;

namespace M011_Authentication.Controllers;

public class HomeController(
	ILogger<HomeController> logger,
	SignInManager<IdentityUser> sim, 
	UserManager<IdentityUser> um, 
	RoleManager<IdentityRole> rm) : Controller  //SignInManager, UserManager und RoleManager werden über builder.Services.AddDefaultIdentity hinzugefügt
{
	public async Task<IActionResult> Index()
	{
		//Aufgabe: Wenn ein User die Index Page angreift, soll dieser ein Admin werden

		//IdentityRole? r = await rm.FindByNameAsync("Admin");

		Task<IdentityRole?> r = rm.FindByNameAsync("Admin"); //Starte die Aufgabe (DB ansprechen)

		IdentityRole? role = await r; //Warte auf das Ergebnis

		if (role == null)
		{
			IdentityRole admin = new IdentityRole("Admin");
			await rm.CreateAsync(admin);
		}

		/////////////////////////////////////////

		IIdentity currentUser = HttpContext.User.Identity;
		if (currentUser.Name == null)
			return View();

		IdentityUser? foundUser = await um.FindByNameAsync(currentUser.Name);
		if (foundUser != null)
		{
			await um.AddToRoleAsync(foundUser, "Admin");
		}

		return View();
	}

	[Authorize(Roles = "Admin")]
	public IActionResult Privacy()
	{
		if (!HttpContext.User.IsInRole("Admin"))
			return Forbid();

		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
