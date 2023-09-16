﻿using CookingRecipes.Data;
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

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}
