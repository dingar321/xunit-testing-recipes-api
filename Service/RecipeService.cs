using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data;
using Recipes.Api.Models;

namespace Recipes.Api.Service;

public interface IRecipeService
{
    Task CreateRecipe(Recipe newRecipe);
    Task EditRecipe(int id, Recipe updatedRecipe);
    Task DeleteRecipeAsync(int id);
    Task<Recipe?> GetRecipe(int id);
    Task<IEnumerable<Recipe>> GetRecipes();
}
    
public class RecipeService : IRecipeService
{
    private readonly DataContext _dataContext;

    public RecipeService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateRecipe(Recipe newRecipe) 
    {
        _dataContext.Recipe.Add(newRecipe);
        await _dataContext.SaveChangesAsync();
    }

    public async Task EditRecipe(int id, Recipe updatedRecipe) 
    {
        var foundRecipe = _dataContext.Recipe.FirstOrDefault(x => x.Id == id);

        if (foundRecipe is not null)
        {
            foundRecipe.RecipeName = updatedRecipe.RecipeName ?? foundRecipe.RecipeName;
            foundRecipe.Description = updatedRecipe.Description ?? foundRecipe.Description;
            foundRecipe.Ingredients = updatedRecipe.Ingredients ?? foundRecipe.Ingredients;
            foundRecipe.Instructions = updatedRecipe.Instructions ?? foundRecipe.Instructions;
        }

        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(int id) 
    {
        var foundRecipe = _dataContext.Recipe.FirstOrDefault(x => x.Id == id);

        if(foundRecipe is not null) 
        {
            _dataContext.Recipe.Remove(foundRecipe);
            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task<Recipe?> GetRecipe(int id) 
    {
        var foundRecipe = await _dataContext.Recipe.FirstOrDefaultAsync(x => x.Id == id);

        if (foundRecipe is not null)
            return foundRecipe;
        else
            return null;
    }

    public async Task<IEnumerable<Recipe>> GetRecipes()
    {
        return await _dataContext.Recipe.ToArrayAsync();
    }
}
