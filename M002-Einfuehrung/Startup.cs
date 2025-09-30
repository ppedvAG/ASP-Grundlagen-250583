namespace M002_Einfuehrung;

public class Startup
{
	public static WebApplication ConfigureServices(WebApplicationBuilder builder)
	{
		builder.Services.AddControllersWithViews();

		//Klasse hier registrieren
		builder.Services.AddSingleton<DependencyInjectionTest>();

		WebApplication app = builder.Build();
		return app;
	}

	public static void ConfigureMiddleware(WebApplication app)
	{
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		app.UseAuthorization();

		app.MapStaticAssets();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}")
			.WithStaticAssets();

		app.Run();
	}
}
