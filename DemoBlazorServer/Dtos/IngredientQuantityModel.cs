using System;
using System.ComponentModel.DataAnnotations;

namespace DemoBlazorServer.Dtos;

public record IngredientQuantityModel
{
    [Required]
    public IngredientModel Ingredient { get; set; } = new();

    [Required]
    [Range(0, int.MaxValue)]
    public double Quantity { get; set; } = 0;
}

