import { Component, OnInit } from '@angular/core';
import { CuisineType } from 'src/app/models/enums/cuisineType';
import { DishType } from 'src/app/models/enums/dishType';
import { RecipeType } from 'src/app/models/enums/recipeType';
import Recipe from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe/recipe.service';

@Component({
  selector: 'app-recipes-list-user',
  templateUrl: './recipes-list-user.component.html',
  styleUrls: ['./recipes-list-user.component.scss']
})
export class RecipesListUserComponent implements OnInit {
  recipes!: Recipe[];

  
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
          this.recipes = recipes;
        },
        error: (response) => {
          console.log(response);
        },
      });
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
