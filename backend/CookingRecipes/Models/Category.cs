using CookingRecipes.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CookingRecipes.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public RecipeType RecipeType { get; set; }
        public ICollection<RecipeCategory>? RecipeCategories { get; set;}
    }
}
