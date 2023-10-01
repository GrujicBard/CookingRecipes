using CookingRecipes.API.Data.Enums;
using CookingRecipes.Data.Enums;
using CookingRecipes.Models;

namespace CookingRecipes.API.Dtos
{
    public class RecipePostDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Instructions { get; set; } = null;
        public string? ImageName { get; set; }
        public string? Ingredients { get; set; }
        public int? Difficulty { get; set; }
        public DishType DishType { get; set; }
        public CuisineType CuisineType { get; set; }
        public ICollection<RecipeCategoryDto>? RecipeCategories { get; set; }
    }
}
