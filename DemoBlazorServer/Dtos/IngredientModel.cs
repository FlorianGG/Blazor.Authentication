using System;
using System.ComponentModel.DataAnnotations;

namespace DemoBlazorServer.Dtos;

public record IngredientModel
{
    [Required]
    public Guid Id { get; set; } = Guid.Empty;

    [Required]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, int.MaxValue)]
    public int Kcal { get; set; } = 0;
}

