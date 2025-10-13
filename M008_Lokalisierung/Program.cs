using M008_Lokalisierung.Middleware;

var builder = WebApplication.CreateBuilder(args);

//////////////////////////////////////////////////////////////////////////////////////////////

//IStringLocalizer hinzufügen (DI)
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources"); //Hier kann ein beliebiger Pfad angegeben werden

//Sprachen registrieren
//Configure: Konfiguriert die Optionen eines Services
//Wird u.a. auch bei Authentication verwendet
builder.Services.Configure<RequestLocalizationOptions>(o =>
{
	string[] supportedCultures = ["de", "en"];
	o.SetDefaultCulture(supportedCultures[0]).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

//////////////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

//Middleware: Code, welcher bei Requests ausgeführt wird
//Kann auch selbst definiert werden (u.a. bei Lokalisierung für Requests)

app.UseRequestLocalization();
app.UseMiddleware<RequestLocalizationMiddleware>(); //Eigene Middleware registrieren

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
