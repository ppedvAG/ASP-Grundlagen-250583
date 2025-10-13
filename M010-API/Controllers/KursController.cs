using M000_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M010_API.Controllers;

[ApiController]
[Route("api/Kurse")]
[Produces("application/json")]
public class KursController(KursDBContext db) : Controller
{
	[HttpGet]
	[Route("AlleKurse")]
	public IEnumerable<Kurse> GetAlleKurse()
	{
		return db.Kurse.ToList();
	}

	[HttpGet]
	[Route("Kurs/{id}")]
	public Kurse GetKurs(int id)
	{
		return db.Kurse.FirstOrDefault(e => e.Id == id);
	}

	[HttpPost]
	[Route("NeuerInhalt/{id}/{inhalt}")]
	public IActionResult PostNeuerInhalt(int id, string inhalt)
	{
		if (!db.Kurse.Any(e => e.Id == id))
		{
			return BadRequest();
		}

		try
		{
			db.KursInhalte.Add(new KursInhalte { KursId = id, InhaltTitel = inhalt });
			db.SaveChanges();
		}
		catch (DbUpdateException ex)
		{
			return BadRequest(ex.Message);
		}

		return Ok();
	}
}
