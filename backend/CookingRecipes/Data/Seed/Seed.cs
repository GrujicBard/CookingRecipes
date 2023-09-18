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

namespace CookingRecipes.Data.Seed
{
    public class Seed
    {
        private readonly DataContext dataContext;
        readonly string _csv_file_path = Path.Combine(AppContext.BaseDirectory, "Assets", "recipes.csv");
        readonly int _numberOfRecipes = 50; // How many recipes to add to database

        public Seed(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SeedDataContext()
        {
            var csvRecipes = ReadCsv(_csv_file_path);
            Random rnd = new();
            List<DishType> dishTypes = Enum.GetValues(typeof(DishType)).Cast<DishType>().ToList();
            List<RecipeType> recipeTypes = Enum.GetValues(typeof(RecipeType)).Cast<RecipeType>().ToList();

            // Recipes
            if (!dataContext.Recipes.Any())
            {
                var recipes = new List<Recipe>();
                int counter = 0;

                foreach (var recipe in csvRecipes)
                {
                    recipes.Add(
                        new Recipe()
                        {
                            Title = recipe.Title,
                            Instructions = recipe.Instructions,
                            ImageName = recipe.ImageName,
                            Ingredients = recipe.Ingredients,
                            Difficulty = rnd.Next(4),
                            DishType = dishTypes[rnd.Next(dishTypes.Count)]
                        });
                    if (++counter > _numberOfRecipes) break;
                }
                dataContext.Recipes.AddRange(recipes);
                dataContext.SaveChanges();
            }

            // Categories
            if (!dataContext.Categories.Any())
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
                dataContext.Categories.AddRange(categories);
                dataContext.SaveChanges();
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
