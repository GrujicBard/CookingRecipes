using CookingRecipes.Dtos;
using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUserTrimToUpper(UserDto userCreate);
        ICollection<Review> GetReviewsByUser(int userId);
        ICollection<Recipe> GetFavoriteRecipesByUser(int userId);
        bool AddFavoriteRecipe(int userId, int recipeId);
        bool RemoveFavoriteRecipe(int userId, int recipeId);
        bool RemoveFavoriteRecipes(int userId);
        bool UserExists(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();

    }
}
