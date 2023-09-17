using System.ComponentModel.DataAnnotations.Schema;

namespace CookingRecipes.Dtos
{
    public class ReviewPostDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }
    }
}
