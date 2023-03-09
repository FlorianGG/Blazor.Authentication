using System;
using System.ComponentModel.DataAnnotations;
namespace DemoBlazorServer.Dtos;

public record PizzaDoughModel
{
    [Required]
    public Guid Id { get; set; } = Guid.Empty;

    [Required]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;
}

