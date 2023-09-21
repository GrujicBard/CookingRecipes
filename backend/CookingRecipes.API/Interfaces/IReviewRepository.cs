using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IReviewRepository
    {
        Task<ICollection<Review>> GetReviews();
        Task<Review> GetReview(int id);
        bool UserReviewExists(int userId, int recipeId);
        Task<ICollection<Review>> GetReviewsOfARecipe(int recipeId);
        bool ReviewExists(int Id);
        Task<bool> CreateReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(Review review);
        Task<bool> DeleteReviews(List<Review> reviews);
        Task<bool> Save();

    }
}
