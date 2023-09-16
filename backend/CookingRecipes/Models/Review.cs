using Microsoft.VisualBasic;

namespace CookingRecipes.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string? Comment { get; set;}
        public int Rating { get; set;}
        public DateTime? PostedDate { get; set; }
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
