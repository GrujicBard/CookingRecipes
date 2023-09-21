using System.ComponentModel.DataAnnotations;

namespace CookingRecipes.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public Role? Role { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserFavoriteRecipe>? UserFavoriteRecipes { get; set; }

    }
}
