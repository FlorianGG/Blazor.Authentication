using System;
using System.ComponentModel.DataAnnotations;

namespace DemoBlazorServer.Dtos;

public class PizzaViewModelValues
{
    [Required]
    public IReadOnlyList<PizzaDoughModel> PizzaDoughs { get; set; } = new List<PizzaDoughModel>().AsReadOnly();

    [Required]
    public IReadOnlyList<IngredientModel> Ingredients{ get; set; } = new List<IngredientModel>().AsReadOnly();
}

