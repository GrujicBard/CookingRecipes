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
using static CookingRecipes.Models.Recipe;

namespace CookingRecipes.Seed
{
    public class Seed
    {
        private readonly DataContext dataContext;
        string csv_file_path = @"C:\Users\Bard\source\repos\CookingRecipes\backend\CookingRecipes\Assets\recipes.csv";

        public Seed(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SeedDataContext()
        {
            var csvRecipes = ReadCsv(csv_file_path);
            Random rnd = new();
            int counter = 0;
            List<DishType> recipeTypes = Enum.GetValues(typeof(DishType))
                .Cast<DishType>()
                .ToList();
            List<CategoryTypes> categoryTypes = Enum.GetValues(typeof(CategoryTypes))
                .Cast<CategoryTypes>()
                .ToList();

            if (!dataContext.Recipes.Any())
            {
                var recipes = new List<Recipe>();

                foreach (var recipe in csvRecipes)
                {
                    recipes.Add(
                        new Recipe()
                        {
                            Title = recipe.Title,
                            Instructions = recipe.Instructions,
                            ImagePath = recipe.ImageName,
                            Ingredients = recipe.Ingredients,
                            Difficulty = rnd.Next(4),
                            RecipeType = recipeTypes[rnd.Next(recipeTypes.Count)],
                            RecipeCategories = new List<RecipeCategory>()
                            {
                                new RecipeCategory() { Category = new Category(){ Name = categoryTypes[rnd.Next(categoryTypes.Count)].ToString() }
                            },
                        }
                        });
                    if (++counter > 12) break;
                }
                dataContext.Recipes.AddRange(recipes);
            }

            if (!dataContext.UserFavoriteRecipes.Any())
            {

                var recipe = csvRecipes.ElementAt(12);
                var favoriteRecipes = new List<UserFavoriteRecipe>()
                {
                    new UserFavoriteRecipe()
                    {
                        Recipe = new Recipe()
                        {
                            Title= recipe.Title,
                            Ingredients= recipe.Ingredients,
                            Instructions= recipe.Instructions,
                            ImagePath = recipe.ImageName,
                            Difficulty = rnd.Next(4),
                            RecipeType = recipeTypes[rnd.Next(recipeTypes.Count)],
                            Reviews = new List<Review>()
                            {
                                new Review() {Comment="Very good recipe!", Rating = 5.0M,
                                    User = new User(){UserName = "jack", Password = "1234", Email = "jack@gmail.com",
                                        Role = new Role(){Name = "User"}}}
                            }
                        },
                        User = new User()
                        {
                            UserName = "bard",
                            Password = "1234",
                            Email = "bard.grujic@gmail.com",
                            Role = new Role()
                            {
                                Name = "Admin"
                            }
                        }

                    }
                };
                dataContext.AddRange(favoriteRecipes);
            }
            dataContext.SaveChanges();

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
                    //Debug.WriteLine(recipes.First().Instructions);
                }
            }
        }


    }
}
