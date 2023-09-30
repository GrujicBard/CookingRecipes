import { MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import Recipe from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe/recipe.service';
import { NotificationService } from 'src/app/services/core/notifications/notification.service';
import { DishType } from 'src/app/models/enums/dishType';
import { CuisineType } from 'src/app/models/enums/cuisineType';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss']
})
export class AddRecipeComponent implements OnInit {

  dishTypes = Object.keys(DishType).filter((item) => {
    return isNaN(Number(item));
  });
  cuisineTypes = Object.keys(CuisineType).filter((item) => {
    return isNaN(Number(item));
  });

  createRecipe!: Recipe;
  categoryId: number;
  private addRecipeSubscription?: Subscription;


  constructor(
    private _recipeService: RecipeService,
    private _notificationService: NotificationService,
    private _dialogRef: MatDialogRef<AddRecipeComponent>,
  ) {
    this.createRecipe = {
      title: "",
      ingredients: "",
      instructions: "",
      imageName: "",
      dishType: 0,
      cuisineType: 0,
      difficulty: 0,
    };
    this.categoryId = 0;
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.addRecipeSubscription?.unsubscribe();
  }

  addRecipe() {
    this.addRecipeSubscription = this._recipeService.addRecipe(this.categoryId, this.createRecipe)
      .subscribe({
        next: () => {
          this._notificationService.openSnackBar("Recipe added!", "Done");
          this._dialogRef.close(true);
        },
        error: (res) => {
          this._notificationService.openSnackBar("There was a problem adding recipe.", "Close");
          console.log(res);
        },
      });
  }

}
