using CsvHelper.Configuration.Attributes;
using static CookingRecipes.Models.Recipe;

namespace CookingRecipes.Data.Seed
{
    public class RecipeSeed
    {
        [Name("Title")]
        public string? Title { get; set; }
        [Name("Cleaned_Ingredients")]
        public string? Ingredients { get; set; }
        [Name("Instructions")]
        public string? Instructions { get; set; } = null;
        [Name("Image_Name")]
        public string? ImageName { get; set; }
    }
}
