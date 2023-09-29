import { MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import Recipe from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe/recipe.service';

@Component({
  selector: 'app-add-edit-recipe',
  templateUrl: './add-edit-recipe.component.html',
  styleUrls: ['./add-edit-recipe.component.scss']
})
export class AddEditRecipeComponent implements OnInit {
  
  dishTypes: string[] = [
    "Breakfast",
    "Brunch",
    "Lunch",
    "Dinner",
  ];

  createRecipe: Recipe;
  categoryId: number;
  private addRecipeSubscription?: Subscription;


  constructor(
    private _recipeService: RecipeService,
    private _router: Router,
    private _dialogRef: MatDialogRef<AddEditRecipeComponent>,
  ) { 
    this.createRecipe ={
      title: "",
      ingredients: "",
      instructions: "",
      imageName: "",
      dishType: 0,
      difficulty: 0,
    };
    this.categoryId = 2;
  }

  ngOnInit() {
  }

  addRecipe() {
    this.addRecipeSubscription = this._recipeService.addRecipe(this.categoryId, this.createRecipe)
      .subscribe({
        next: () => {
          alert("Recipe added successfuly");
          this._dialogRef.close(true);
          location.reload()
        },
        error: (res) => {
          console.log(res);
          alert("There was a problem adding Recipe");
        },
      });
  }

}
