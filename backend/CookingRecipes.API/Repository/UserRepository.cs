using CookingRecipes.Data;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipes.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFavoriteRecipe(int userId, int recipeId)
        {

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefaultAsync();

            var userFavoriteRecipe = new UserFavoriteRecipe()
            {
                User = user,
                Recipe = recipe,
            };

            await _context.AddAsync(userFavoriteRecipe);
            return await Save();
        }

        public async Task<bool> CreateUser(User user)
        {
            await _context.AddAsync(user);
            return await Save();
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Remove(user);
            return await Save();
        }

        public async Task<ICollection<Recipe>> GetFavoriteRecipesByUser(int userId)
        {
            return await _context.UserFavoriteRecipes.Where(u => u.UserId == userId).Select(r => r.Recipe).ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsByUser(int userId)
        {
            return await _context.Reviews.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _context.Users.OrderBy(u => u.UserName).ToListAsync();
        }

        public async Task<bool> RemoveFavoriteRecipe(int userId, int recipeId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefaultAsync();

            var favoriteRecipe = await _context.UserFavoriteRecipes.Where(f => f.UserId == userId && f.RecipeId == recipeId).FirstOrDefaultAsync();

            _context.Remove(favoriteRecipe);
            return await Save();
        }

        public async Task<bool> RemoveFavoriteRecipes(int userId)
        {
            var favoriteRecipes = await _context.UserFavoriteRecipes.Where(f => f.UserId == userId).ToListAsync();
            _context.RemoveRange(favoriteRecipes);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Update(user);
            return await Save();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
        public bool UserNameExists(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }
        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
