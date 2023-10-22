using CookingRecipes.API.Dtos;
using CookingRecipes.API.Models.Jwt;
using CookingRecipes.Dtos;
using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUserByEmail(string email);
        Task<ICollection<Review>> GetReviewsByUser(int userId);
        Task<ICollection<Recipe>> GetFavoriteRecipesByUser(int userId);
        Task<bool> AddFavoriteRecipe(int userId, int recipeId);
        Task<bool> RemoveFavoriteRecipe(int userId, int recipeId);
        Task<bool> RemoveFavoriteRecipes(int userId);
        bool UserExists(int userId);
        bool UserNameExists(string username);
        bool EmailExists(string email);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<bool> Register(RegisterDto user);
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> Save();


    }
}
