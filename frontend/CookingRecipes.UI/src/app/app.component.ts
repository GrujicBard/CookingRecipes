import { Component } from '@angular/core';
import Recipe from './models/recipe.model';
import { RecipeService } from './services/recipe/recipe.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  title = 'CookingRecipes.UI';

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
  }

}


