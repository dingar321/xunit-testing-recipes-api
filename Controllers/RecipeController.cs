﻿using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Models;
using Recipes.Api.Service;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("recipes")] 
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    /// <summary>
    /// Creates a new recipe
    /// </summary>
    /// <response code="201"> Created a new recipe </response>
    /// <response code="400"> The body for a new recipe was not valid </response>
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] RecipeDto newRecipe)
    {
        await _recipeService.CreateRecipe(
            new Recipe
            {
                RecipeName = newRecipe.RecipeName,
                Description = newRecipe.Description,
                Ingredients = newRecipe.Ingredients,
                Instructions = newRecipe.Instructions
            });

        return Ok(); 
    }

    /// <summary>
    /// Updates a specified recipe
    /// </summary>
    /// <response code="200"> Specified Recipe was updated </response>
    /// <response code="400"> The body for the updated recipe was not valid </response>
    [HttpPost]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Edit(
        [FromBody] RecipeDto updateRecipe,
        [FromQuery] int id) 
    {

        await _recipeService.EditRecipe(
            id,
            new Recipe
            {
                RecipeName = updateRecipe.RecipeName,
                Description = updateRecipe.Description,
                Ingredients = updateRecipe.Ingredients,
                Instructions = updateRecipe.Instructions
            });

        return Ok();
    }
}
