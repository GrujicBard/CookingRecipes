using CookingRecipes.Data;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;

namespace CookingRecipes.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(r => r.PostedDate).ToList();
        }

        public ICollection<Review> GetReviewsOfARecipe(int recipeId)
        {
            return _context.Reviews.Where(r => r.RecipeId == recipeId).ToList();
        }

        public bool ReviewExists(int Id)
        {
            return _context.Reviews.Any(r => r.Id == Id);
        }
    }
}
