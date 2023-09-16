namespace CookingRecipes.Models
{
    public class UserFavoriteRecipe
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public Recipe? Recipe { get; set; }
        public User? User { get; set; }
    }
}
