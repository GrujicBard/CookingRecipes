using CookingRecipes.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CookingRecipes.Models
{
    public class Recipe
    {
        [Key]
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Instructions { get; set; } = null;
        public string? ImageName { get; set;}
        public string? Ingredients { get; set; }
        public int? Difficulty { get; set; }
        public DishType DishType { get; set; }
        public ICollection<UserFavoriteRecipe>? UserFavoriteRecipes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<RecipeCategory>? RecipeCategories { get; set; }
    }
}
