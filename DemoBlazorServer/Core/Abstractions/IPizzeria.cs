using System;
using DemoBlazorServer.Dtos;

namespace DemoBlazorServer.Core.Abstractions;

public interface IPizzeria
{
    public Task StorePizzeria();

    public Task InitPizzeria();

    public IReadOnlyList<PizzaModel> GetPizzas();

    public Task DeletePizza(Guid id);

    public Task<PizzaModel> EditPizza(PizzaFormModel pizzaToEdit);

    public Task<PizzaModel> CreatePizza(PizzaFormModel pizzaToCreate);

    public IReadOnlyList<IngredientModel> GetIngredients();

    public Task DeleteIngredient(Guid id);

    public Task<IngredientModel> EditIngredient(IngredientModel ingredientToEdit);

    public Task<IngredientModel> CreateIngredient(IngredientModel ingredientToCreate);

    public IReadOnlyList<PizzaDoughModel> GetPizzaDoughs();

    public Task DeletePizzaDough(Guid id);

    public Task<PizzaDoughModel> EditPizzaDough(PizzaDoughModel pizzaDoughToEdit);

    public Task<PizzaDoughModel> CreatePizzaDough(PizzaDoughModel pizzaDoughToCreate);
}

