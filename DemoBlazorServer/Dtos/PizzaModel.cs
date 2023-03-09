using System;
using System.ComponentModel.DataAnnotations;
using DemoBlazorServer.Enums;

namespace DemoBlazorServer.Dtos;

public record PizzaModel
{
    [Required]
    public Guid Id { get; set; } = Guid.Empty;

    [Required]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public PizzaDoughModel PizzaDough { get; set; } = new();

    [Required]
    [MinLength(1)]
    public List<IngredientQuantityModel> IngredientQuantities { get; set; } = new();

    [Required]
    public PizzaSizes Size { get; set; }

}

