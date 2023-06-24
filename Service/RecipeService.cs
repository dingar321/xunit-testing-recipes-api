using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data;
using Recipes.Api.Models;

namespace Recipes.Api.Service;

public interface IRecipeService
{
    Task CreateRecipe(string recipeName, string description, string ingredients, string instructions);
    Task EditRecipe(int id, string recipeName, string description, string ingredients, string instructions);
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

    public async Task CreateRecipe(
        string recipeName, string description, 
        string ingredients, string instructions) 
    {
        var newRecipe = new Recipe
        {
           RecipeName = recipeName,
           Description = description,   
           Ingredients = ingredients,
           Instructions = instructions
        };

        _dataContext.Recipe.Add(newRecipe);
        await _dataContext.SaveChangesAsync();
    }

    public async Task EditRecipe(
        int id, string recipeName, string description,
        string ingredients, string instructions) 
    {
        var foundRecipe = _dataContext.Recipe.FirstOrDefault(x => x.Id == id);

        if (foundRecipe is not null)
        {
            foundRecipe.RecipeName = recipeName ?? foundRecipe.RecipeName;
            foundRecipe.Description = description ?? foundRecipe.RecipeName;
            foundRecipe.Ingredients = ingredients ?? foundRecipe.RecipeName;
            foundRecipe.Instructions = instructions ?? foundRecipe.RecipeName;
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
