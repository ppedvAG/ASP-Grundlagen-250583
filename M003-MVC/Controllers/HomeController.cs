using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M003_MVC.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
	public IActionResult Index() => View();

	public IActionResult Privacy() => View();

	public IActionResult DateiUpload(IFormFile file)
	{
		if (!Directory.Exists("Upload"))
			Directory.CreateDirectory("Upload");

		using Stream readStream = file.OpenReadStream();
		using FileStream fs = new FileStream(Path.Combine("Upload", file.FileName), FileMode.Create);
		readStream.CopyTo(fs);
		fs.Flush();

		return View("Index");
	}



	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
