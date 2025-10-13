using M001.Models;
using Microsoft.EntityFrameworkCore;

namespace M001;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		string? connString = builder.Configuration.GetConnectionString("Northwind"); //CS auslesen
		if (connString != null)
			builder.Services.AddDbContext<NorthwindContext>(o => o.UseSqlServer(connString)); //CS mitgeben

		var app = builder.Build();

		// Configure the HTTP request pipeline.
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
			pattern: "{controller}/{action}/{id?}")
			.WithStaticAssets();

		app.Run();
	}
}
