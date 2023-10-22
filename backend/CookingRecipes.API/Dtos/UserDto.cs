using CookingRecipes.Data.Enums;
using CookingRecipes.Models;

namespace CookingRecipes.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
        public RoleType? Role { get; set; }
    }
}
