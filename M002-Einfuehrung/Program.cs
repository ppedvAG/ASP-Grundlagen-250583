using M002_Einfuehrung;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Dependency Injection
//Beliebige C#-Klassen werden hier angemeldet (mithilfe von den Add-Methoden)
//In den Konstruktoren der Controller können diese Objekte empfangen werden
builder.Services.AddControllersWithViews();

//Klasse hier registrieren
builder.Services.AddSingleton<DependencyInjectionTest>();

WebApplication app = builder.Build();

////////////////////////////////////////////////////////////////////////

//Middleware
//Pipeline, welche jeder Request durchgehen muss
//Bei jeder Methode wird der entsprechende Code auf den Request angewandt
//WICHTIG: Reihenfolge bei Middleware (Use-Methoden) bestimmt die Reihenfolge, in der die Methoden abgearbeitet werden

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
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.Run();

/////////////////////////////////////////////

WebApplicationBuilder b = WebApplication.CreateBuilder(args);
WebApplication a = Startup.ConfigureServices(b);
Startup.ConfigureMiddleware(a);