using M000_DataAccess.Models;
using M006_ModelBinding.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace M006_ModelBinding.Controllers;

public class HomeController(ILogger<HomeController> logger, KursDBContext db) : Controller
{
	//Dieses Property kann bei jeder Action mitgegeben werden
	//Ist dementsprechend allgemeingültig definiert (Index, Test und Privacy)
	[FromQuery]
	public string? TestString { get; set; }

	public IActionResult Index()
	{
		return View(new User());
	}

	[Route("/Home/Test")]
	public IActionResult Test([Bind] User u)
	{
		//[Bind]: Wenn Werte übergeben werden, welche vom Namen her mit den Namen in
		//der Klasse übereinstimmen, werden diese direkt in das Objekt hinein geschrieben
		return View("Index", u);
	}

	/// <summary>
	/// Linq
	/// SQL in C#
	/// Wird bei EF verwendet, um die Daten aus der DB zu holen
	/// </summary>
	public IActionResult Privacy()
	{
		//IQueryable
		//Ableitung von IEnumerable
		//-> Nur eine Anleitung
		db.Kurse.Where(e => e.Id == 1); //Ohne ToList() werden keine Daten geladen

		//List<Kurse> k = q.ToList(); //Hier werden die Daten geladen

		db.Kurse.First(e => e.Id == 1);

		db.Kurse.OrderBy(e => e.Kursname); //Ohne ToList() werden keine Daten geladen

		//Sobald die Daten in der C#-Anwendung sind (von der DB geladen sind), können beliebige Linq-Statements ausgeführt werden
		db.Kurse.All(e => e.Aktiv == 1);

		db.Kurse.Any(e => e.Aktiv == 1);

		////////////////////////////////////////////////////

		//Rohe SQL-Statements auf die DB werfen
		int x = db.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM KursDB.dbo.Kurse");

		//Neue Entities hinzufügen
		db.Kurse.Add(new Kurse() { });

		//Mit SaveChanges() werden die Änderungen in die DB übernommen
		db.SaveChanges();

		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
