using CookingRecipes.Data;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using Microsoft.EntityFrameworkCore;


namespace CookingRecipes.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        public RecipeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRecipe(Recipe recipe)
        {
            await _context.AddAsync(recipe);
            return await Save();
        }

        public async Task<bool> DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
            return await Save();
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            return await _context.Recipes.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Recipe> GetRecipeByTitle(string title)
        {
            return await _context.Recipes.Where(r => r.Title.Trim().ToUpper() == title.Trim().ToUpper()).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetRecipeRating(int id)
        {
            var reviews = await _context.Reviews.Where(r => r.RecipeId == id).ToListAsync();

            if (reviews.Count() <= 0)
            {
                return 0;
            }

            return reviews.Sum(r => r.Rating) / reviews.Count();
        }

        public async Task<ICollection<Recipe>> GetRecipes()
        {
            return await _context.Recipes.Include(r => r.RecipeCategories).ThenInclude(rc => rc.Category).ToListAsync();
        }

        public async Task<ICollection<Recipe>> GetRecipesByCategory(int categoryId)
        {
            return await _context.RecipeCategories.Where(rc => rc.CategoryId == categoryId).Select(c => c.Recipe).ToListAsync();
        }


        public bool RecipeExists(int id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }

        public bool RecipeTitleExists(string title)
        {
            return _context.Recipes.Any(r => r.Title == title);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRecipe(Recipe recipe)
        {
            _context.Update(recipe);
            return await Save();
        }

        public async Task<ICollection<RecipeCategory>> GetRecipeCategoriesByRecipeId(int recipeId)
        {
            return await _context.RecipeCategories.Where(rc => rc.RecipeId == recipeId).ToListAsync();
        }

        public async Task<bool> DeleteRecipeCategoriesByRecipeId(int recipeId)
        {
            var recipeCategories = await _context.RecipeCategories.Where(rc => rc.RecipeId == recipeId).ToListAsync();
            _context.RemoveRange(recipeCategories);
            return await Save();
        }
    }
}
