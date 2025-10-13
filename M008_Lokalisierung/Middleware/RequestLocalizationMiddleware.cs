using System.Globalization;

namespace M008_Lokalisierung.Middleware;

/// <summary>
/// Eigene Middleware
/// 
/// </summary>
public class RequestLocalizationMiddleware
{
	/// <summary>
	/// Delegate: Methodenzeiger
	/// 
	/// Hält hier die nächste Methode in der Middleware-Kette
	/// </summary>
	private readonly RequestDelegate next;

	private readonly ILogger logger;

	public RequestLocalizationMiddleware(RequestDelegate next, ILogger<RequestLocalizationMiddleware> logger)
	{
		this.next = next;
		this.logger = logger;
	}

	/// <summary>
	/// Diese Methode macht die Arbeit der eigenen Middleware
	/// 
	/// Diese Methode führt am Ende von sich selbst die nächste Middleware aus
	/// </summary>
	public async Task InvokeAsync(HttpContext context)
	{
		//logger.Log(LogLevel.Information, "Hallo von der Middleware");

		string? path = context.Request.Path.Value;
		if (!string.IsNullOrEmpty(path))
		{
			string[] segments = path.Split('?', StringSplitOptions.RemoveEmptyEntries);
			if (segments.Length > 1)
			{
				string queryParameters = segments[1];

				string[] parameters = queryParameters.Split("&");

				string lang = parameters.First(e => e.Contains("lang"));

				string language = lang.Split("=")[1];

				CultureInfo ci = new CultureInfo(language);
				CultureInfo.CurrentCulture = ci;
				CultureInfo.CurrentUICulture = ci;
			}
		}

		await next(context); //Führt die nächste Methode in der Hierarchie aus (hier HttpsRedirection)
	}
}