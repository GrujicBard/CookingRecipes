using CookingRecipes.Data;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;

namespace CookingRecipes.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Recipe> GetRecipeByCategory(int categoryId)
        {
            return _context.RecipeCategories.Where(rc  => rc.CategoryId == categoryId).Select(c => c.Recipe).ToList();
        }
    }
}
