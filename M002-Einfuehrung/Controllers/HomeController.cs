using M002_Einfuehrung.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M002_Einfuehrung.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	private DependencyInjectionTest DITest { get; set; }

	/// <summary>
	/// Hier werden die in der Program.cs angemeldeten Objekte empfangen (in Form von Parametern)
	/// Diese Parameter werden in Variablen geschrieben, damit diese in den Handler-Methoden verwendet werden können
	/// 
	/// Es müssen nicht alle angemeldeten Objekte empfangen werden
	/// </summary>
	public HomeController(ILogger<HomeController> logger, DependencyInjectionTest diTest)
	{
		_logger = logger;
		DITest = diTest;
	}

	public IActionResult Index()
	{
		_logger.Log(LogLevel.Information, "Hallo Welt");
		return View();
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
}
