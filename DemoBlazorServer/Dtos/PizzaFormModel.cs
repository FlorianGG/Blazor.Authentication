using System;
using System.ComponentModel.DataAnnotations;
using DemoBlazorServer.Enums;

namespace DemoBlazorServer.Dtos;

public class PizzaFormModel
{
    [Required]
    public Guid Id { get; set; } = Guid.Empty;

    [Required]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public Guid PizzaDoughId { get; set; } = Guid.Empty;

    [Required]
    [MinLength(1)]
    public List<IngredientQuantityFormModel> IngredientQuantities { get; set; } = new();

    [Required]
    public PizzaSizes Size { get; set; }
}


