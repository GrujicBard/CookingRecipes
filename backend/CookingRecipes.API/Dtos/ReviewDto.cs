using System.ComponentModel.DataAnnotations.Schema;

namespace CookingRecipes.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string? Comment { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }
        public DateTime? PostedDate { get; set; }
    }
}
