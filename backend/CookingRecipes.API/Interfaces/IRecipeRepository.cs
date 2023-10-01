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
        Task<ICollection<Category>> GetCategoriesByRecipeId(int recipeId);
        Task<ICollection<RecipeCategory>> GetRecipeCategoriesByRecipeId(int recipeId);
        Task<bool> AddRecipeCategories(int recipeId, IList<int> categoryIds);
        Task<bool> AddRecipeCategoriesByRecipeTitle(string title, IList<int> categoryIds);
        Task<bool> RemoveRecipeCategories(int recipeId, IList<int> categoryIds);
        Task<bool> RemoveRecipeCategory(int recipeId, int categoryId);
        Task<bool> DeleteRecipeCategories(List<RecipeCategory> recipeCategories);
        bool RecipeExists(int id);
        bool RecipeTitleExists(string title);
        Task<bool> CreateRecipe(Recipe recipe);
        Task<bool> UpdateRecipe(Recipe recipe);
        Task<bool> DeleteRecipe(Recipe recipe);
        Task<bool> Save();
    }
}
