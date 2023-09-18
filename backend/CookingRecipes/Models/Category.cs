using CookingRecipes.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CookingRecipes.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public RecipeType RecipeType { get; set; }
        public ICollection<RecipeCategory>? RecipeCategories { get; set;}
    }
}
