using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingRecipes.Models
{
    public class Review
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public string? Comment { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Rating { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? PostedDate { get; set; } = DateTime.Now;
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
