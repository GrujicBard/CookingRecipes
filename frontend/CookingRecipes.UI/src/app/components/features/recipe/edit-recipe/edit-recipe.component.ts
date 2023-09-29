import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import Recipe from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe/recipe.service';
import { NotificationService } from 'src/app/services/core/notifications/notification.service';

@Component({
  selector: 'app-edit-recipe',
  templateUrl: './edit-recipe.component.html',
  styleUrls: ['./edit-recipe.component.scss']
})
export class EditRecipeComponent implements OnInit {

  dishTypes: string[] = [
    "Breakfast",
    "Brunch",
    "Lunch",
    "Dinner",
  ];

  displayRecipe!: Recipe;
  private edditRecipeSubscription?: Subscription;


  constructor(
    private _recipeService: RecipeService,
    private _notificationService: NotificationService,
    private _dialogRef: MatDialogRef<EditRecipeComponent>,
    @Inject(MAT_DIALOG_DATA) data: any,
  ) {

    this.displayRecipe = { ...data };
    this.displayRecipe.difficulty = data.difficulty.toString();
  }

  ngOnInit() {
  }

  editRecipe() {
    this.edditRecipeSubscription = this._recipeService.updateRecipe(this.displayRecipe)
      .subscribe({
        next: () => {
          this._notificationService.openSnackBar("Recipe updated!", "Done");
          this._dialogRef.close(true);
        },
        error: (res) => {
          console.log(res);
          this._notificationService.openSnackBar("There was a problem deleting recipe.", "Close");
        },
      });
  }

}
