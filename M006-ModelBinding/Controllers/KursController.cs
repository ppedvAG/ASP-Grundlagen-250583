using M000_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace M006_ModelBinding.Controllers;

public class KursController(KursDBContext db) : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult AlleKurse()
	{
		IQueryable<Kurse> q = db.Kurse;

		return View(q);
	}

	public IActionResult KursDetails(int id)
	{
		IQueryable<KursInhalte> q = db.KursInhalte.Where(e => e.KursId == id);
		Kurse k = db.Kurse.Single(e => e.Id == id);
		return View((q, k.Kursname)); //Tupel: Gruppierung von Typen ohne eine Klasse erstellen zu müssen
	}

	public IActionResult NeuerInhalt(int id)
	{
		Kurse k = db.Kurse.Single(e => e.Id == id);
		return View((id, k.Kursname));
	}

	public IActionResult InhaltSpeichern(int id, string inhalt)
	{
		db.KursInhalte.Add(new KursInhalte() { KursId = id, InhaltTitel = inhalt });
		db.SaveChanges();
		return RedirectToAction("KursDetails", new { id = id });
	}
}
