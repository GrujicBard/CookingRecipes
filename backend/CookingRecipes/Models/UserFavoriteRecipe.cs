using System.ComponentModel.DataAnnotations.Schema;

namespace CookingRecipes.Models
{
    public class UserFavoriteRecipe
    {
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Recipe? Recipe { get; set; }
        public User? User { get; set; }
    }
}
