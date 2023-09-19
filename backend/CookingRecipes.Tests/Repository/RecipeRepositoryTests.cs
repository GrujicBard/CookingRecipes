using CookingRecipes.Data;
using CookingRecipes.Models;
using CookingRecipes.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingRecipes.Tests.Repository
{
    public class RecipeRepositoryTests
    {
        Random rnd = new();
        private async Task<DataContext> GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Recipes.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Recipes.Add(
                        new Recipe()
                        {
                            Title = "pancakes",
                            Ingredients = "randomIngredients",
                            Instructions = "randomStuff",
                            DishType = Data.Enums.DishType.Lunch,
                            ImageName = "image" + i.ToString(),
                            Difficulty = rnd.Next(4),
                            RecipeCategories = new List<RecipeCategory>()
                            {
                                new RecipeCategory()
                                {
                                    Category = new Category(){ RecipeType = Data.Enums.RecipeType.Rice}
                                }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review()
                                {
                                    Comment = "great",
                                    Rating = rnd.Next(6),
                                    User = new User()
                                    {
                                        UserName = "John",
                                        Email = "john@gmail.com",
                                        Role = new Role()
                                        {
                                            RoleType = Data.Enums.RoleType.User
                                        }

                                    }
                                }
                            }
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void RecipeRepository_GetRecipe_ReturnsRecipe()
        {
            #region Arrange
            var title = "pancakes";
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            var result = recipeRepository.GetRecipe(title);
            #endregion

            #region Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Recipe>();
            #endregion
        }

        [Fact]
        public async void RecipeRepository_GetRecipeRating_ReturnDecimaBetweenOneAndFive()
        {
            #region Arrange
            var recipeId = 1;
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            var result = recipeRepository.GetRecipeRating(recipeId);
            #endregion

            #region Assert
            result.Should().NotBe(0);
            result.Should().BeInRange(1, 5);
            #endregion
        }
    }
}
