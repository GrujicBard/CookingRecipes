import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CuisineType } from 'src/app/models/enums/cuisineType';
import { DishType } from 'src/app/models/enums/dishType';
import { RecipeType } from 'src/app/models/enums/recipeType';
import Recipe from 'src/app/models/recipe.model';
import { RecipeService } from 'src/app/services/recipe/recipe.service';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.scss']
})
export class RecipeDetailsComponent implements OnInit {
  private recipeId: number;
  recipeDetails: Recipe;
  ingredients: string[] = [];
  instructions: string[] = [];
  constructor(
    private route: ActivatedRoute,
    private recipeService: RecipeService,
  ) {
    this.recipeId = 0;
    this.recipeDetails = {
      title: "",
      ingredients: "",
      instructions: "",
      imageName: "",
      dishType: 0,
      cuisineType: 0,
      difficulty: 0,
      recipeCategories: [],
    };
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.recipeId = params["id"];
    });
    this.getRecipe(this.recipeId);
  }


  getRecipe(recipeId: number) {
    this.recipeService.getRecipeById(recipeId)
      .subscribe({
        next: (recipe) => {
          this.recipeDetails = recipe;
          this.ingredients = recipe.ingredients.split("', '");
          this.instructions = recipe.instructions.split(/[\r\n]+/)
        },
        error: (res) => {
          console.log(res);
        }
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

  public numSequence(n: number): Array<number> { 
    return Array(n); 
  } 

}
