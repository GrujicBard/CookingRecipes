using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CookingRecipes.API.Dtos
{
    public class RegisterDto
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
