using Microsoft.AspNetCore.Mvc;

namespace M009_LayoutDesign.Views.Shared.Components;

public class DateViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		DateTime today = DateTime.Now;

		return View(today);
	}
}
