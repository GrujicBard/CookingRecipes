using CookingRecipes.Dtos;
using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IRecipeRepository
    {
        Task<ICollection<Recipe>> GetRecipes();
        Task<Recipe> GetRecipeById(int id);
        Task<Recipe> GetRecipeByTitle(string title);
        Task<decimal> GetRecipeRating(int id);
        Task<ICollection<Recipe>> GetRecipesByCategory(int categoryId);
        bool RecipeExists(int id);
        bool RecipeTitleExists(string title);
        Task<bool> CreateRecipe(int categoryId, Recipe recipe);
        Task<bool> UpdateRecipe(Recipe recipe);
        Task<bool> DeleteRecipe(Recipe recipe);
        Task<bool> Save();
    }
}
