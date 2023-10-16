import { Component, OnInit } from '@angular/core';
import Category from 'src/app/models/category';
import { CuisineType } from 'src/app/models/enums/cuisineType';
import { DishType } from 'src/app/models/enums/dishType';
import { RecipeType } from 'src/app/models/enums/recipeType';
import Recipe from 'src/app/models/recipe';
import RecipeCategory from 'src/app/models/recipeCategory';
import { RecipeService } from 'src/app/services/recipe/recipe.service';

@Component({
  selector: 'app-recipes-list-user',
  templateUrl: './recipes-list-user.component.html',
  styleUrls: ['./recipes-list-user.component.scss']
})
export class RecipesListUserComponent implements OnInit {
  dishTypes = Object.keys(DishType).filter((item) => {
    return isNaN(Number(item));
  });
  cuisineTypes = Object.keys(CuisineType).filter((item) => {
    return isNaN(Number(item));
  });
  recipeTypes = Object.keys(RecipeType).filter((item) => {
    return isNaN(Number(item));
  });


  recipes!: Recipe[];
  searchValue: string = "";
  recipeType: string = "";
  dishType: string = "";
  cuisineType: string = "";


  constructor(
    private _recipeService: RecipeService,
  ) { }

  ngOnInit() {
    this.getRecipes();
  }

  getRecipes() {
    this._recipeService.getRecipes()
      .subscribe({
        next: (recipes) => {
          console.log(recipes);
          this.recipes = recipes;
          this.filterRecipes();
        },
        error: (response) => {
          console.log(response);
        },
      });
  }

  onSelect() {
    this.getRecipesByTitle();
  }


  getRecipesByTitle() {
    if (this.searchValue == "") {
      this.getRecipes();
    }
    else {
      this._recipeService.getRecipesByTitle(this.searchValue)
        .subscribe({
          next: (recipes) => {
            this.recipes = recipes;
            this.filterRecipes();
          },
          error: (response) => {
            console.log(response);
          },
        });
    }

  }

  filterRecipes() {

    // Filter Category
    if (this.recipeType) {
      this.recipes = this.recipes.filter(({ recipeCategories }) =>
        recipeCategories.find(recipeCategories => recipeCategories.category?.recipeType === this.recipeTypes.indexOf(this.recipeType)
        ));
    }

    // Filter Dish Type
    if (this.dishType) {
      this.recipes = this.recipes.filter((recipe) =>
        recipe.dishType === this.dishTypes.indexOf(this.dishType)
      );
    }
    // Filter Cuisine
    if (this.cuisineType) {
      this.recipes = this.recipes.filter((recipe) =>
        recipe.cuisineType === this.cuisineTypes.indexOf(this.cuisineType)
      );
    }
  }


  public get DishType() {
    return DishType;
  }

  public get CuisineType() {
    return CuisineType;
  }

  public get RecipeType() {
    return RecipeType;
  }
}
