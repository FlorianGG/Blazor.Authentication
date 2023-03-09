using System;
using System.ComponentModel.DataAnnotations;

namespace DemoBlazorServer.Dtos;

public class IngredientQuantityFormModel
{
    [Required]
    public Guid IngredientId { get; set; } = Guid.Empty;

    [Required]
    [Range(0, int.MaxValue)]
    public double Quantity { get; set; } = 0;
}

