import { Component } from '@angular/core';
import Recipe from './models/recipe';
import { RecipeService } from './services/recipe/recipe.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  title = 'CookingRecipes.UI';
  recipes: Recipe[] = [];
  recipeToEdit?: Recipe;

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
    this.recipeService
      .getRecipes()
      .subscribe((result: Recipe[]) => (this.recipes = result));
  }

  initNewRecipe() {
    this.recipeToEdit = {
      title: "",
      ingredients: "",
      imageName: "",
      instructions: "",
      difficulty: 0,
      dishType: 0,
      cuisineType: 0,
    };
  }
  editRecipe(recipe: Recipe) {
    this.recipeToEdit = recipe;
  }
}
