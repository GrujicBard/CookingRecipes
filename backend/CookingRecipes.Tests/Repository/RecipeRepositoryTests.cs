using CookingRecipes.Data;
using CookingRecipes.Models;
using CookingRecipes.Repository;
using FakeItEasy;
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
        private async Task<DataContext> GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            Random rnd = new();
            if (await databaseContext.Recipes.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Recipes.Add(
                        new Recipe()
                        {
                            Title = "recipe" + i.ToString(),
                            Ingredients = "randomIngredients",
                            Instructions = "randomStuff",
                            DishType = Data.Enums.DishType.Lunch,
                            ImageName = "image",
                            Difficulty = 2,
                            RecipeCategories = new List<RecipeCategory>()
                            {
                                new RecipeCategory()
                                {
                                    Category = new Category(){ RecipeType = Data.Enums.RecipeType.Beef}
                                },

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

        [Theory]
        [InlineData("recipe1")]
        [InlineData("recipe2")]
        [InlineData("recipe3")]
        public async void RecipeRepository_GetRecipeByTitle_ReturnsRecipe(string title)
        {
            #region Arrange
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            var result = recipeRepository.GetRecipeByTitle(title);
            #endregion

            #region Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Recipe>();
            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void RecipeRepository_GetRecipeById_ReturnsRecipe(int recipeId)
        {
            #region Arrange
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            var result = recipeRepository.GetRecipeById(recipeId);
            #endregion

            #region Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Recipe>();
            #endregion
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        public async void RecipeRepository_GetRecipesByCategory_ReturnsList(int categoryId)
        {
            #region Arrange
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            var result = recipeRepository.GetRecipesByCategory(categoryId);
            #endregion

            #region Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Recipe>>();
            result.Count.Should().BeGreaterThan(0);
            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void RecipeRepository_GetRecipeRating_ReturnsDecimaBetweenOneAndFive(int recipeId)
        {
            #region Arrange
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

        [Fact]
        public async void RecipeRepository_GetRecipes_ReturnsList()
        {
            #region Arrange
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            var result = recipeRepository.GetRecipes();
            #endregion

            #region Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Recipe>>();
            result.Count.Should().BeGreaterThan(0);
            #endregion
        }

        [Fact]
        public async void RecipeRepository_CreateRecipe_ReturnsTrue()
        {
            #region Arrange
            var recipe = new Recipe()
            {
                Title = "pancakes",
                Ingredients = "eggs, flour",
                Instructions = "mix flour and eggs",
                DishType = Data.Enums.DishType.Breakfast,
                ImageName = "image200",
                Difficulty = 3,
            };
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            var result = recipeRepository.CreateRecipe(10, recipe);
            #endregion

            #region Assert
            result.Should().BeTrue();
            #endregion
        }

        [Fact]
        public async void RecipeRepository_DeleteRecipe_ReturnsTrue()
        {
            #region Arrange
            var recipe = new Recipe()
            {
                Title = "pancakes",
                Ingredients = "eggs, flour",
                Instructions = "mix flour and eggs",
                DishType = Data.Enums.DishType.Breakfast,
                ImageName = "image200",
                Difficulty = 3,
            };
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            recipeRepository.CreateRecipe(2, recipe);
            var result = recipeRepository.DeleteRecipe(recipe);
            #endregion

            #region Assert
            result.Should().BeTrue();
            #endregion
        }

        [Fact]
        public async void RecipeRepository_UpdateRecipe_ReturnsTrue()
        {
            #region Arrange
            var recipe = new Recipe()
            {
                Title = "pancakes",
                Ingredients = "eggs, flour",
                Instructions = "mix flour and eggs",
                DishType = Data.Enums.DishType.Breakfast,
                ImageName = "image200",
                Difficulty = 3,
            };
            var updatedrecipe = recipe;
            updatedrecipe.Title = "cheesecake";
            var dbContext = await GetDataBaseContext();
            var recipeRepository = new RecipeRepository(dbContext);
            #endregion

            #region Act
            recipeRepository.CreateRecipe(2, recipe);
            var result = recipeRepository.UpdateRecipe(updatedrecipe);
            #endregion

            #region Assert
            result.Should().BeTrue();
            #endregion
        }
    }
}
