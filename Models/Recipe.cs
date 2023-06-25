using System.ComponentModel.DataAnnotations;

namespace Recipes.Api.Models;

public class Recipe
{
    [Key] public int Id { get; set; }
    [Required] public required string RecipeName { get; set; }
    [Required] public required string Description { get; set; }
    [Required] public required string Ingredients { get; set; }
    [Required] public required string Instructions { get; set; }
}

public class RecipeDto
{
    [Required] public required string RecipeName { get; set; }
    [Required] public required string Description { get; set; }
    [Required] public required string Ingredients { get; set; }
    [Required] public required string Instructions { get; set; }
}