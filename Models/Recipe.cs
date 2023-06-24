using System.ComponentModel.DataAnnotations;

namespace Recipes.Api.Models;

public class Recipe
{
    [Key] public int Id { get; set; }
    [Required] public string RecipeName { get; set; } = null!;
    [Required] public string Description { get; set; } = null!;
    [Required] public string Ingredients { get; set; } = null!;
    [Required] public string Instructions { get; set; } = null!;
}
