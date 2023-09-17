using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        Recipe GetRecipe(string title);
        decimal GetRecipeRating(int id);
        bool RecipeExists(int id);
        bool CreateRecipe(int categoryId, Recipe recipe);
        bool CreateUserFavoriteRecipe(int userId, int recipeId);
        bool Save();
    }
}
