using CookingRecipes.Data;
using CookingRecipes.Data.Enums;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using Microsoft.EntityFrameworkCore;

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

        public bool CategoryTypeExists(RecipeType recipeType)
        {
            return _context.Categories.Any(c => c.RecipeType == recipeType);
        }

        public async Task<bool> CreateCategory(Category category)
        {
            await _context.AddAsync(category);
            return await Save();
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            _context.Remove(category);
            return await Save();
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            return await _context.Categories.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _context.Update(category);
            return await Save();
        }
    }
}
