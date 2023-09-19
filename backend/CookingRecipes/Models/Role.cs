using CookingRecipes.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CookingRecipes.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public RoleType RoleType { get; set; }
        public ICollection<User>? Users { get; set;}

    }
}
