import { Component, OnInit } from '@angular/core';
import Recipe from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css']
})
export class RecipesListComponent implements OnInit {

  recipes: Recipe[] = [];
  constructor(private recipeService: RecipeService ) {}

  ngOnInit() {
    this.recipeService.getRecipes()
    .subscribe({
      next:(recipes) =>{
        this.recipes = recipes;
      },
      error:(response)=>{
        console.log(response);
      },
    });
  }

}
