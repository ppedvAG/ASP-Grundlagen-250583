using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Rechner;

public class ErgebnisModel : PageModel
{
	public int Ergebnis;

	public void OnGet(int Ergebnis)
	{
		this.Ergebnis = Ergebnis;
	}
}
