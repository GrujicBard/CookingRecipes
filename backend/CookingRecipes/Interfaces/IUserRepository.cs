using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        ICollection<Review> GetReviewsByUser(int userId);
        ICollection<Recipe> GetFavoriteRecipesByUser(int userId);
        bool UserExists(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool Save();

    }
}
