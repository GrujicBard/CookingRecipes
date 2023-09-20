using CookingRecipes.Dtos;
using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<ICollection<Review>> GetReviewsByUser(int userId);
        Task<ICollection<Recipe>> GetFavoriteRecipesByUser(int userId);
        Task<bool> AddFavoriteRecipe(int userId, int recipeId);
        Task<bool> RemoveFavoriteRecipe(int userId, int recipeId);
        Task<bool> RemoveFavoriteRecipes(int userId);
        bool UserExists(int userId);
        bool UserNameExists(string username);
        bool EmailExists(string email);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> Save();

    }
}
