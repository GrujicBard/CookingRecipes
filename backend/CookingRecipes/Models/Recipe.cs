namespace CookingRecipes.Models
{
    public class Recipe
    {
        private DishType recipeType;

        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Instructions { get; set; } = null;
        public string? ImagePath { get; set;}
        public string? Ingredients { get; set; }
        public int? Difficulty { get; set; }
        public DishType RecipeType { get => recipeType; set => recipeType = value; }
        public ICollection<UserFavoriteRecipe>? UserFavoriteRecipes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<RecipeCategory>? RecipeCategories { get; set; }


        public enum DishType
        {
            Breakfast = 0,
            Lunch = 1,
            Dinner = 2,
            Snack = 3,
        }

        //public enum CategoryTypes
        //{
        //    Soups = 0,
        //    Salads = 1,
        //    Burgers = 2,
        //    Pizza = 3,
        //    Rice = 4,
        //    Seafood = 5,
        //    Beef = 6,
        //    Chicken = 7,
        //    Pork = 8,
        //}
    }
}
