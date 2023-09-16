using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfARecipe(int recipeId);
        bool ReviewExists(int Id);

    }
}
