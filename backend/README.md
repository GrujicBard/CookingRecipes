# Backend

![image](https://github.com/GrujicBard/CookingRecipes/assets/33715866/5cf04a3d-19ae-4d5c-8b7a-dd50348feb53)

## Database Setup
### Create Database
Place the SQL Server connection string in appsettings.json.

Type the following commands into Package Manager Console to create tables in database:

`Add-Migration InitialCreate`

`Update-Database`
### Seed Database
Recipes data is from [Kaggle: Food Ingredients and Recipes Dataset with Images](https://pages.github.com/](https://www.kaggle.com/datasets/pes12017000148/food-ingredients-and-recipe-dataset-with-images)https://www.kaggle.com/datasets/pes12017000148/food-ingredients-and-recipe-dataset-with-images/).

Rename the .csv file to "recipes.csv". Create an "\Assets" folder in "\backend\CookingRecipes.API" and place it inside.

In the terminal go to folder "\backend\CookingRecipes.API".

Type the following command into terminal to seed the database:

`dotnet run seeddata`

This will add the recipes from the recipes.csv file to the database.

