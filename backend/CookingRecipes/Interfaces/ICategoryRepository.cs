using CookingRecipes.Data.Enums;
using CookingRecipes.Dtos;
using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        bool CategoryExists(int id);
        bool CategoryTypeExists(RecipeType recipeType);
        Task<bool> CreateCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(Category category);
        Task<bool> Save();

    }
}
