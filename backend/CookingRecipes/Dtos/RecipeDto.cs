using CookingRecipes.Data.Enums;

namespace CookingRecipes.Dtos
{
    public class RecipeDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Instructions { get; set; } = null;
        public string? ImageName { get; set; }
        public string? Ingredients { get; set; }
        public int? Difficulty { get; set; }
        public DishType DishType { get; set; }
    }
}
