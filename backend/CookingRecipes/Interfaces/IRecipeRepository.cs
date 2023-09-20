using CookingRecipes.Dtos;
using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        Recipe GetRecipeById(int id);
        Recipe GetRecipeByTitle(string title);
        decimal GetRecipeRating(int id);
        ICollection<Recipe> GetRecipesByCategory(int categoryId);
        bool RecipeExists(int id);
        bool CreateRecipe(int categoryId, Recipe recipe);
        bool UpdateRecipe(Recipe recipe);
        bool DeleteRecipe(Recipe recipe);
        bool Save();
    }
}
