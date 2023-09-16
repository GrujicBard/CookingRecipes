namespace CookingRecipes.Dtos
{
    public class RecipeDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Instructions { get; set; } = null;
        public string? ImagePath { get; set; }
        public string? Ingredients { get; set; }
        public int? Difficulty { get; set; }
    }

    public enum DishType
    {
        Breakfast = 0,
        Lunch = 1,
        Dinner = 2,
        Snack = 3,
    }
}
