import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipesListComponent } from '../recipes-list/recipes-list.component';
import { RecipeService } from 'src/app/services/recipe.service';
import Recipe from 'src/app/models/recipe';

@Component({
  selector: 'edit-recipe',
  templateUrl: './edit-recipe.component.html',
  styleUrls: ['./edit-recipe.component.css']
})
export class EditRecipeComponent implements OnInit {
  dishTypes: string[] = [
    "Breakfast",
    "Brunch",
    "Lunch",
    "Dinner",
  ];

  recipeDetails: Recipe = {
    title: "",
    ingredients: "",
    instructions: "",
    imageName: "",
    dishType: 0,
    difficulty: 1,
  };

  constructor(
    private route: ActivatedRoute,
    private recipeService: RecipeService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id: number = Number(params.get("id"));

        if (id) {
          this.recipeService.getRecipeById(id)
            .subscribe({
              next: (response) => {
                this.recipeDetails = response;
              }
            });
        }
      }
    })
  }

  updateRecipe() {
    this.recipeService.updateRecipe(this.recipeDetails)
      .subscribe({
        next: () => {
          this.router.navigate(['recipes']);
        }
      });
  }

  deleteRecipe() {
    this.recipeService.deleteRecipe(this.recipeDetails)
      .subscribe({
        next: () => {
          this.router.navigate(['recipes']);
        },
        error:(response)=>{
          console.log(response);
          this.router.navigate(['recipes']);
        }
      });
  }


}
