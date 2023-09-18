using CookingRecipes.Data;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;

namespace CookingRecipes.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        public RecipeRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRecipe(int categoryId, Recipe recipe)
        {
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var recipeCategory = new RecipeCategory()
            {
                Category = category,
                Recipe = recipe,
            };

            _context.Add(recipeCategory);
            _context.Add(recipe);

            return Save();
        }

        public bool DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
            return Save();
        }

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.Where(r => r.Id == id).FirstOrDefault();
        }

        public Recipe GetRecipe(string title)
        {
            return _context.Recipes.Where(r => r.Title == title).FirstOrDefault();
        }

        public decimal GetRecipeRating(int id)
        {
            var review = _context.Reviews.Where(r => r.RecipeId == id);

            if (review.Count() <= 0)
            {
                return 0;
            }

            return review.Sum(r => r.Rating) / review.Count();
        }

        public ICollection<Recipe> GetRecipes()
        {
            return _context.Recipes.OrderBy(r => r.Id).ToList();
        }

        public bool RecipeExists(int id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateRecipe(Recipe recipe)
        {
            _context.Update(recipe);
            return Save();
        }
    }
}
