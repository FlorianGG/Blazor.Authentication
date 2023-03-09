using System;
using DemoBlazorServer.Dtos;
using DemoBlazorServer.Core.Abstractions;

namespace DemoBlazorServer.Services.ViewModels;

public class PizzaViewModel
{
    private readonly IPizzeria pizzeria;

    public PizzaViewModel(IPizzeria pizzeria)
    {
        this.pizzeria = pizzeria;
    }

    public PizzaViewModelValues GetInitialValues()
    {
        return new PizzaViewModelValues()
        {
            Ingredients = this.pizzeria.GetIngredients(),
            PizzaDoughs = this.pizzeria.GetPizzaDoughs()

        };
    }
 
}

