using M000_DataAccess.Models;
using M005_EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M005_EntityFramework.Controllers;

/// <summary>
/// Entity Framework Core
/// 
/// Verbindung zu einer Datenbank und anschließende Modellierung von den Tabellen zu C#-Klassen
/// 
/// VS Extension: EF Core Power Tools
/// Rechtsklick auf das Projekt -> EF Core Power Tools -> Reverse Engineer
/// 
/// Connection String anlegen -> Tabellen/Views/Procedures auswählen -> DbContext und Model Klassen erzeugen
/// 
/// NuGet Pakete:
/// - Microsoft.EntityFrameworkCore
/// - Microsoft.EntityFrameworkCore.Design
/// - Microsoft.EntityFrameworkCore.Tools
/// - EFCore Paket für die Datenbank (hier Microsoft.EntityFrameworkCore.SqlServer)
/// 
/// Um auf die Datenbank zuzugreifen, benötigen wir hier ein Objekt vom Typ KursDBContext
/// Der DbContext wird hier über Dependency Injection verfügbar gemacht (siehe Program.cs)
/// </summary>
public class HomeController(ILogger<HomeController> logger, KursDBContext db) : Controller
{
	public IActionResult Index()
	{
		//DbSet
		//Stellt eine Tabelle in der DB dar
		//Über eine Linq-Syntax werden die Daten angegriffen
		//Das Linq-Statement wird zu einem SQL-Statement umgewandelt
		//Die Daten, die als Ergebnis zurückkommen, werden vom EF zu C# Objekte konvertiert

		//Alle Kurse von der DB holen
		List<Kurse> kurse = db.Kurse.ToList(); //ToList(): Lädt die Daten

		return View(kurse);
	}

	public IActionResult Privacy() => View();

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}