using CookingRecipes.Data.Enums;

namespace CookingRecipes.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public RecipeType RecipeType { get; set; }
    }
}
