using System;
using System.Text.Json;
using DemoBlazorServer.Core.Abstractions;
using DemoBlazorServer.Dtos;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DemoBlazorServer.Services.Datas;


public class Pizzeria : IPizzeria
{

    private List<PizzaModel> pizzas = new();

    private List<IngredientModel> ingredients = new();

    private List<PizzaDoughModel> pizzaDoughs = new();
    private readonly ProtectedLocalStorage protectedStorage;

    public Pizzeria(ProtectedLocalStorage protectedStorage)
    {
        this.protectedStorage = protectedStorage;
    }

    public async Task StorePizzeria()
    {
        var newPizzeria = new ProtectedPizzeriaStored()
        {
            pizzas = this.pizzas,
            pizzaDoughs = this.pizzaDoughs,
            ingredients = this.ingredients
        };

        await this.protectedStorage.SetAsync("pizzeria", JsonSerializer.Serialize<ProtectedPizzeriaStored>(newPizzeria));
    }

    public async Task InitPizzeria()
    {

        var unserializedResult = await this.protectedStorage.GetAsync<string>("pizzeria");

        if (unserializedResult.Success)
        {
            if (unserializedResult.Value != null)
            {
                var result = JsonSerializer.Deserialize<ProtectedPizzeriaStored>(unserializedResult.Value);
                this.pizzas = result?.pizzas ?? new();
                this.ingredients = result?.ingredients ?? new();
                this.pizzaDoughs = result?.pizzaDoughs ?? new();
            }
        }
    }

    public IReadOnlyList<IngredientModel> GetIngredients()
    {
        return this.ingredients.AsReadOnly();
    }

    public async Task DeleteIngredient(Guid id)
    {
        ingredients.RemoveAll(pd => pd.Id == id);

        await StorePizzeria();
    }

    public async Task<IngredientModel> EditIngredient(IngredientModel ingredientToEdit)
    {
        var ingredient = ingredients.First(pd => pd.Id == ingredientToEdit.Id) with { };

        await this.DeleteIngredient(ingredientToEdit.Id);

        ingredients.Add(ingredientToEdit);

        await StorePizzeria();

        return ingredientToEdit;
    }

    public async Task<IngredientModel> CreateIngredient(IngredientModel ingredientToCreate)
    {
        var ingredient = ingredients.FirstOrDefault(pd => pd.Id == ingredientToCreate.Id);

        if (ingredient != null)
            throw new Exception("Ingrédient déjà créé");

        ingredients.Add(ingredientToCreate);

        await StorePizzeria();

        return ingredientToCreate;
    }

    public IReadOnlyList<PizzaDoughModel> GetPizzaDoughs()
    {
        return this.pizzaDoughs.AsReadOnly();
    }

    public async Task DeletePizzaDough(Guid id)
    {
        pizzaDoughs.RemoveAll(pd => pd.Id == id);
        await StorePizzeria();
    }

    public async Task<PizzaDoughModel> EditPizzaDough(PizzaDoughModel pizzaDoughToEdit)
    {
        var pizzaDough = pizzaDoughs.First(pd => pd.Id == pizzaDoughToEdit.Id) with { };

        await this.DeletePizzaDough(pizzaDoughToEdit.Id);

        pizzaDoughs.Add(pizzaDoughToEdit);

        await StorePizzeria();

        return pizzaDoughToEdit;
    }

    public async Task<PizzaDoughModel> CreatePizzaDough(PizzaDoughModel pizzaDoughToCreate)
    {
        var pizzaDough = pizzaDoughs.FirstOrDefault(pd => pd.Id == pizzaDoughToCreate.Id);

        if (pizzaDough != null)
            throw new Exception("Pâte à pizza déjà créée");

        pizzaDoughs.Add(pizzaDoughToCreate);

        await StorePizzeria();

        return pizzaDoughToCreate;
    }

    public IReadOnlyList<PizzaModel> GetPizzas()
    {
        return this.pizzas.AsReadOnly();
    }

    public async Task DeletePizza(Guid id)
    {
        pizzas.RemoveAll(p => p.Id == id);
        await StorePizzeria();
    }

    public async Task<PizzaModel> EditPizza(PizzaFormModel pizzaToEdit)
    {
        var pizza = pizzas.First(p => p.Id == pizzaToEdit.Id) with { };

        await this.DeletePizza(pizzaToEdit.Id);

        var completePizzaEdited = this.CheckPizzaToCreateOrEdit(pizzaToEdit);

        pizzas.Add(completePizzaEdited);

        await StorePizzeria();

        return completePizzaEdited;
    }

    public async Task<PizzaModel> CreatePizza(PizzaFormModel pizzaToCreate)
    {
        var pizza = pizzaDoughs.FirstOrDefault(p => p.Id == pizzaToCreate.Id);

        if (pizza != null)
            throw new Exception("Pizza déjà créée");

        var completePizzaToCreate = this.CheckPizzaToCreateOrEdit(pizzaToCreate);

        pizzas.Add(completePizzaToCreate);

        await StorePizzeria();

        return completePizzaToCreate;
    }

    private PizzaModel CheckPizzaToCreateOrEdit(PizzaFormModel pizzaToEditOrCreate)
    {
        // check de la PizzaDough
        var pizzaDough = pizzaDoughs.First(pd => pd.Id == pizzaToEditOrCreate.PizzaDoughId);

        if (pizzaDough == null)
            throw new Exception("La pâte à pizza est introuvable");

        var ingredientQuantities = new List<IngredientQuantityModel>();
        // vérification des ingrédients
        foreach (var ingredientQuantity in pizzaToEditOrCreate.IngredientQuantities)
        {
            var ingredient = ingredients.First(i => i.Id == ingredientQuantity.IngredientId);
            if (ingredient == null)
            {
                throw new Exception("Ingrédient introuvable");
            }
            ingredientQuantities.Add(
                new IngredientQuantityModel()
                {
                    Ingredient = ingredient,
                    Quantity = ingredientQuantity.Quantity
                }
            );
        }

        var completePizzaEdited = new PizzaModel()
        {
            Id = pizzaToEditOrCreate.Id,
            Name = pizzaToEditOrCreate.Name,
            PizzaDough = pizzaDough,
            IngredientQuantities = ingredientQuantities,
            Size = pizzaToEditOrCreate.Size
        };

        return completePizzaEdited;
    }
}


