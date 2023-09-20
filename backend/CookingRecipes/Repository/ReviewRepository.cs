using CookingRecipes.Data;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipes.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            return await Save();
        }

        public async Task<bool> DeleteReview(Review review)
        {
            _context.Remove(review);
            return await Save();
        }

        public async Task<bool> DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return await Save();
        }

        public async Task<Review> GetReview(int id)
        {
            return await _context.Reviews.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Review>> GetReviews()
        {
            return await _context.Reviews.OrderBy(r => r.PostedDate).ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsOfARecipe(int recipeId)
        {
            return await _context.Reviews.Where(r => r.RecipeId == recipeId).ToListAsync();
        }

        public async Task<Review> GetUserReviewOfARecipe(int userId, int recipeId)
        {
            return await _context.Reviews.Where(r => r.UserId == userId && r.RecipeId == recipeId).FirstOrDefaultAsync();
        }

        public bool ReviewExists(int Id)
        {
            return _context.Reviews.Any(r => r.Id == Id);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateReview(Review review)
        {
            _context.Update(review);
            return await Save();
        }
    }
}
