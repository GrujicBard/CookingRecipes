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
            List<DishType> recipeTypes = Enum.GetValues(typeof(DishType))
                .Cast<DishType>()
                .ToList();

            int counter = 0;

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
                            RecipeType = recipeTypes[rnd.Next(recipeTypes.Count)]
                        });
                    if (++counter > _numberOfRecipes) break;
                }
                dataContext.Recipes.AddRange(recipes);
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
