using CookingRecipes.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CookingRecipes.Models
{
    public class User
    {
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        public string? UserName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public RoleType Role { get; set; } = RoleType.User;
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserFavoriteRecipe>? UserFavoriteRecipes { get; set; }

    }
}
