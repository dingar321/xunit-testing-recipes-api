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

public static class RecipeMapper 
{
    public static Recipe MapToDomain(this RecipeDto recipeDto)
    {
        return new Recipe
        {
            RecipeName = recipeDto.RecipeName,
            Description = recipeDto.Description,
            Ingredients = recipeDto.Ingredients,
            Instructions = recipeDto.Instructions
        };
    }

    public static RecipeDto MapToDto(this Recipe recipe)
    {
        return new RecipeDto
        {
            RecipeName = recipe.RecipeName,
            Description = recipe.Description,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions
        };
    }
}

