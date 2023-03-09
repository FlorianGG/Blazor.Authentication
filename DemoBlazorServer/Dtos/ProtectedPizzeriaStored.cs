using System;
namespace DemoBlazorServer.Dtos;

public class ProtectedPizzeriaStored
{
    public List<PizzaModel> pizzas { get; set; } = new();

    public List<IngredientModel> ingredients { get; set; } = new();

    public List<PizzaDoughModel> pizzaDoughs { get; set; } = new();
}

