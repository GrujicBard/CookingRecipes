using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfARecipe(int recipeId);
        bool ReviewExists(int Id);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool Save();

    }
}
