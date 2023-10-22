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
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Instructions { get; set; } = string.Empty;
        public string ImageName { get; set;} = string.Empty;
        [Required]
        public string Ingredients { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 0;
        public DishType DishType { get; set; } = 0;
        public CuisineType CuisineType { get; set; } = 0;
        public ICollection<UserFavoriteRecipe>? UserFavoriteRecipes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<RecipeCategory>? RecipeCategories { get; set; }
    }
}
