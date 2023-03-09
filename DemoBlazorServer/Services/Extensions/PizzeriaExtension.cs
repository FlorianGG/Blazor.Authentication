using System;
using DemoBlazorServer.Core.Abstractions;
using DemoBlazorServer.Services.Datas;

namespace DemoBlazorServer.Services.Extensions;

public static class PizzeriaExtension
{
    public static void AddPizzeria(this IServiceCollection services)
    {
        services.AddScoped<IPizzeria, Pizzeria>();
    }
}

