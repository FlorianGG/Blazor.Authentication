using System;
namespace DemoBlazorServer.Services.Extensions;

public static class LocalizationAndCultureExtension
{
	public static void AddLocalizationLanguagesAndCulture(this IServiceCollection services)
	{
		services.AddLocalization();
        services.AddControllers();
    }

	public static void CulturesRegistration(this WebApplication app)
	{
        var supportedCultures = new[] { "en-US", "fr-FR" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[1])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
        app.MapControllers();
    }
}

