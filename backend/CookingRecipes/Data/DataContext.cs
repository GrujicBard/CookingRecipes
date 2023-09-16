using CookingRecipes.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CookingRecipes.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserFavoriteRecipe> UserFavoriteRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeCategory>()
                .HasKey(rc => new { rc.RecipeId, rc.CategoryId });
            modelBuilder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Recipe)
                .WithMany(r => r.RecipeCategories)
                .HasForeignKey(rc => rc.RecipeId);
            modelBuilder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Category)
                .WithMany(c => c.RecipeCategories)
                .HasForeignKey(rc => rc.CategoryId);

            modelBuilder.Entity<UserFavoriteRecipe>()
                .HasKey(ufr => new { ufr.RecipeId, ufr.UserId });
            modelBuilder.Entity<UserFavoriteRecipe>()
                .HasOne(ufr => ufr.Recipe)
                .WithMany(r => r.UserFavoriteRecipes)
                .HasForeignKey(ufr => ufr.RecipeId);
            modelBuilder.Entity<UserFavoriteRecipe>()
                .HasOne(ufr => ufr.User)
                .WithMany(u => u.UserFavoriteRecipes)
                .HasForeignKey(ufr => ufr.UserId);
        }
    }
}
