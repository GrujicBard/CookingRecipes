using CookingRecipes.API.Data.Enums;
using CookingRecipes.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CookingRecipes.Models
{
    public class Recipe
    {
        public int? Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Instructions { get; set; } = null;
        public string? ImageName { get; set;}
        [Required]
        public string? Ingredients { get; set; }
        [DefaultValue(0)]
        public int? Difficulty { get; set; }
        public DishType DishType { get; set; }
        public CuisineType CuisineType { get; set; }
        public ICollection<UserFavoriteRecipe>? UserFavoriteRecipes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<RecipeCategory>? RecipeCategories { get; set; }
    }
}
