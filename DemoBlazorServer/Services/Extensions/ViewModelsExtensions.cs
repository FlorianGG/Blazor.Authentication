using System;
namespace DemoBlazorServer.Services.Extensions;

using DemoBlazorServer.Services.ViewModels;

public static class ViewModelsExtensions
{
	public static void AddViewModels(this IServiceCollection services)
	{
		services.AddScoped<PizzaViewModel>();
	}
}

