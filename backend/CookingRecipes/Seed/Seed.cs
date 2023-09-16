using System.Data;
using System.Text.Json;
using System.Diagnostics;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using CookingRecipes.Models;
using System.Diagnostics;

namespace CookingRecipes.Seed
{
    public class Seed
    {
        string csv_file_path = @"C:\Users\Bard\source\repos\CookingRecipes\CookingRecipes\Assets\recipes.csv";

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
                    return csvReader.GetRecords<RecipeSeed>();
                    //Debug.WriteLine(recipes.First().Instructions);

                }
            }
        }

    }
}
