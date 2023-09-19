using CookingRecipes.Data;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;

namespace CookingRecipes.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool AddFavoriteRecipe(int userId, int recipeId)
        {

            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            var recipe = _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefault();

            var userFavoriteRecipe = new UserFavoriteRecipe()
            {
                User = user,
                Recipe = recipe,
            };

            _context.Add(userFavoriteRecipe);
            return Save();
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public ICollection<Recipe> GetFavoriteRecipesByUser(int userId)
        {
            return _context.UserFavoriteRecipes.Where(u => u.UserId == userId).Select(r => r.Recipe).ToList();
        }

        public ICollection<Review> GetReviewsByUser(int userId)
        {
            return _context.Reviews.Where(u => u.UserId == userId).ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserName).ToList();
        }

        public User GetUserTrimToUpper(UserDto userCreate)
        {
            return _context.Users.Where(r => r.UserName.Trim().ToUpper() == userCreate.UserName.Trim().ToUpper()
                || r.Email.Trim().ToUpper() == userCreate.Email.Trim().ToUpper()).FirstOrDefault();
        }

        public bool RemoveFavoriteRecipe(int userId, int recipeId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            var recipe = _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefault();

            var favoriteRecipe = _context.UserFavoriteRecipes.Where(f => f.UserId == userId && f.RecipeId == recipeId).FirstOrDefault();

            _context.Remove(favoriteRecipe);
            return Save();
        }

        public bool RemoveFavoriteRecipes(int userId)
        {
            var favoriteRecipes = _context.UserFavoriteRecipes.Where(f => f.UserId == userId).ToList();
            _context.RemoveRange(favoriteRecipes);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}
