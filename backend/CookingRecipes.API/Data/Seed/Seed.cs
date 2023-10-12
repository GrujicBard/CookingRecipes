using System.Data;
using System.Text.Json;
using System.Diagnostics;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using CookingRecipes.Models;
using CookingRecipes.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections;
using CookingRecipes.Dtos;
using CookingRecipes.Data.Enums;
using CookingRecipes.API.Data.Enums;

namespace CookingRecipes.Data.Seed
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        readonly string _csv_file_path = Path.Combine(AppContext.BaseDirectory, "Assets", "recipes.csv");
        readonly int _numberOfRecipes = 50; // How many recipes to add to database

        public Seed(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public void SeedDataContext()
        {
            var csvRecipes = ReadCsv(_csv_file_path);
            Random rnd = new();
            List<DishType> dishTypes = Enum.GetValues(typeof(DishType)).Cast<DishType>().ToList();
            List<CuisineType> cuisineTypes = Enum.GetValues(typeof(CuisineType)).Cast<CuisineType>().ToList();
            List<RecipeType> recipeTypes = Enum.GetValues(typeof(RecipeType)).Cast<RecipeType>().ToList();
            List<RoleType> roleTypes = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().ToList();

            // Recipes
            if (!_dataContext.Recipes.Any())
            {
                var recipes = new List<Recipe>();
                int counter = 0;

                foreach (var recipe in csvRecipes)
                {
                    string ingredients = recipe.Ingredients;
                    ingredients = ingredients.Substring(2, ingredients.Length - 4);
                    recipes.Add(
                        new Recipe()
                        {
                            Title = recipe.Title,
                            Instructions = recipe.Instructions,
                            ImageName = recipe.ImageName,
                            Ingredients = ingredients,
                            Difficulty = rnd.Next(1,4),
                            DishType = dishTypes[rnd.Next(dishTypes.Count)],
                            CuisineType = cuisineTypes[rnd.Next(cuisineTypes.Count)],
                        });
                    if (++counter == _numberOfRecipes) break;
                }
                _dataContext.Recipes.AddRange(recipes);
                _dataContext.SaveChanges();
            }

            // Categories
            if (!_dataContext.Categories.Any())
            {
                var categories = new List<Category>();

                foreach (var type in recipeTypes)
                {
                    categories.Add(
                        new Category()
                        {
                            RecipeType = type
                        });
                }
                _dataContext.Categories.AddRange(categories);
                _dataContext.SaveChanges();
            }

            // RecipeCategories
            if (!_dataContext.RecipeCategories.Any())
            {
                var recipeCategores = new List<RecipeCategory>();

                for (int i = 1; i < _numberOfRecipes + 1; i++)
                {
                    recipeCategores.Add(
                        new RecipeCategory()
                        {
                            RecipeId = i,
                            CategoryId = rnd.Next(1, recipeTypes.Count() + 1),

                        });
                }
                _dataContext.RecipeCategories.AddRange(recipeCategores);
                _dataContext.SaveChanges();
            }

            // Users
            if (!_dataContext.Users.Any())
            {
                var users = new List<User>()
                {
                    new User()
                    {
                        UserName = "Janez",
                        Email = "janez@gmail.com",
                        Role = new Role()
                        {
                            RoleType = RoleType.User,
                        }

                    },
                    new User()
                    {
                        UserName = "Bard",
                        Email = "bard@gmail.com",
                        Role = new Role()
                        {
                            RoleType = RoleType.Admin,
                        }

                    }
                };
                _dataContext.Users.AddRange(users);
                _dataContext.SaveChanges();
            }

            if (!_dataContext.Reviews.Any())
            {
                // Reviews
                var reviews = new List<Review>()
                    {
                        new Review()
                        {
                            UserId = 1,
                            RecipeId = 1,
                            Rating = 4,
                            Comment = "Not Bad!",
                        },
                         new Review()
                        {
                            UserId = 1,
                            RecipeId = 2,
                            Rating = 5,
                            Comment = "Excellent recipe!",
                        },
                        new Review()
                        {
                            UserId = 1,
                            RecipeId = 3,
                            Rating = 2,
                            Comment = "I didn't really like the taste.",
                        },
                        new Review()
                        {
                            UserId = 2,
                            RecipeId = 1,
                            Rating = 3,
                            Comment = "It was okay.",
                        },
                        new Review()
                        {
                            UserId = 2,
                            RecipeId = 2,
                            Rating = 4.5M,
                            Comment = "It came out wonderfully!",
                        },
                        new Review()
                        {
                            UserId = 2,
                            RecipeId = 3,
                            Rating = 1,
                            Comment = "I hated it.",
                        },
                    };
                _dataContext.Reviews.AddRange(reviews);
                _dataContext.SaveChanges();
            }

            // UserFavoriteRecipes
            if (!_dataContext.UserFavoriteRecipes.Any())
            {
                var userFavoriteRecipes = new List<UserFavoriteRecipe>();

                for (int i = 1; i < _numberOfRecipes/2 + 1; i++)
                {
                    userFavoriteRecipes.Add(
                        new UserFavoriteRecipe()
                        {
                            RecipeId = i,
                            UserId = rnd.Next(1, 3),

                        });
                }
                _dataContext.UserFavoriteRecipes.AddRange(userFavoriteRecipes);
                _dataContext.SaveChanges();
            }
        }

        public IEnumerable<RecipeSeed> ReadCsv(string csv_file_path)
        {

            using (var streamReader = new StreamReader(csv_file_path))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ","
                };
                using (var csvReader = new CsvReader(streamReader, csvConfig))
                {
                    return csvReader.GetRecords<RecipeSeed>().ToList();
                }
            }
        }


    }
}
