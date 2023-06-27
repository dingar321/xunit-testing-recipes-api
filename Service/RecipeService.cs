using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data;
using Recipes.Api.Models;

namespace Recipes.Api.Service;

public interface IRecipeService
{
    Task CreateRecipe(RecipeDto newRecipeDto);
    Task EditRecipe(int id, RecipeDto updatedRecipeDto);
    Task DeleteRecipe(int id);
    Task<RecipeDto?> GetRecipe(int id);
    Task<IEnumerable<RecipeDto>> GetRecipes();
}
    
public class RecipeService : IRecipeService
{
    private readonly DataContext _dataContext;

    public RecipeService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateRecipe(RecipeDto newRecipeDto) 
    {
        _dataContext.Recipe.Add(newRecipeDto.MapToDomain());
        await _dataContext.SaveChangesAsync();
    }

    public async Task EditRecipe(int id, RecipeDto updatedRecipeDto) 
    {
        var foundRecipe = _dataContext.Recipe.FirstOrDefault(x => x.Id == id);

        if (foundRecipe is not null)
        {
            foundRecipe.RecipeName = updatedRecipeDto.RecipeName ?? foundRecipe.RecipeName;
            foundRecipe.Description = updatedRecipeDto.Description ?? foundRecipe.Description;
            foundRecipe.Ingredients = updatedRecipeDto.Ingredients ?? foundRecipe.Ingredients;
            foundRecipe.Instructions = updatedRecipeDto.Instructions ?? foundRecipe.Instructions;
        }

        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteRecipe(int id) 
    {
        var foundRecipe = _dataContext.Recipe.FirstOrDefault(x => x.Id == id);

        if(foundRecipe is not null) 
        {
            _dataContext.Recipe.Remove(foundRecipe);
            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task<RecipeDto?> GetRecipe(int id) 
    {
        var foundRecipe = await _dataContext.Recipe.FirstOrDefaultAsync(x => x.Id == id);

        if (foundRecipe is not null)
            return foundRecipe.MapToDto();
        else
            return null;
    }

    public async Task<IEnumerable<RecipeDto>> GetRecipes()
    {
        var recipesList = await _dataContext.Recipe.ToArrayAsync();
        return recipesList.Select(recipe => recipe.MapToDto());
    }
}
