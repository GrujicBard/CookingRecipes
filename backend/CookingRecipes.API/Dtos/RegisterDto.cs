using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CookingRecipes.API.Dtos
{
    public class RegisterDto
    {
        public int Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
