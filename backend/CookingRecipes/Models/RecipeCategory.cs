using System.ComponentModel.DataAnnotations.Schema;

namespace CookingRecipes.Models
{
    public class RecipeCategory
    {
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Recipe? Recipe { get; set; }
        public Category? Category { get; set; }

    }
}
