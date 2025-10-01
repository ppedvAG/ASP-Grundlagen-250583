using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace M004_RazorPages.Pages.Rechner;

public class RechnerModel : PageModel
{
	public int AnzWerte;

	public void OnGet(string Zahl)
	{
		if (string.IsNullOrWhiteSpace(Zahl))
			AnzWerte = 0;
		else
			AnzWerte = int.Parse(Zahl);
	}

	public IActionResult OnPostZahlSetzen(string zahl)
	{
		if (string.IsNullOrWhiteSpace(zahl) || !int.TryParse(zahl, out int z))
			return BadRequest();

		return RedirectToPage("Rechner", new { Zahl = z });
	}

	public IActionResult OnPostBerechne(Rechenoperation op, string[] zahlen)
	{
		List<int> ints = [];
		foreach (string str in zahlen)
			if (!string.IsNullOrWhiteSpace(str) && int.TryParse(str, out int z))
				ints.Add(z);

		//int[] ints = zahlen.Select(int.Parse).ToArray();

		int gesamt = ints[0];
		foreach (int i in ints.Skip(1))
		{
			switch (op)
			{
				case Rechenoperation.Addition:
					gesamt += i;
					break;
				case Rechenoperation.Subtraktion:
					gesamt -= i;
					break;
				case Rechenoperation.Multiplikation:
					gesamt *= i;
					break;
				case Rechenoperation.Division:
					gesamt /= i;
					break;
			}
		}

		return RedirectToPage("Ergebnis", new { Ergebnis = gesamt });
	}
}