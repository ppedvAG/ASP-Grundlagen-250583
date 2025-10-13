using M011_Authentication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

///////////////////////////////////////////////////////////////////////////////////////////////////

//Verbindung zu einer Standard-DB herstellen, welche die Userdaten enth�lt
//Kann auch in eine eigene (bestehende) Datenbank gelegt werden
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//IdentityUser: Standardklasse f�r Benutzer
//Enth�lt Username, Passwort, Email, TelNr, 2FA
//Kann auch durch eine eigene Klasse ausgetauscht werden
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = true;

	//Useranforderungen konfigurieren
	options.User.RequireUniqueEmail = true;
	//options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

	//Passwortanforderungen konfigurieren
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;

}).AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>();

//SignInManager
//Allg. Userverwaltung (Einloggen, Registrieren, ...)

//UserManager
//Gibt Informationen �ber den User

//RoleManager
//Erm�glicht die Verwaltung von Rollen (= Rechten)
//Muss per DI angemeldet werden (.AddRoles<IdentityRole>())

//Identity Pages erzeugen: Rechtsklick aufs Projekt -> Add -> New Scaffolded Item -> Identity (linke Seite)

///////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
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

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
