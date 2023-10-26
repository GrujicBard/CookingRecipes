import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import Recipe from 'src/app/models/recipe.model';
import { RecipeService } from 'src/app/services/recipe/recipe.service';
import { NotificationService } from 'src/app/services/core/notifications/notification.service';
import { DishType } from 'src/app/models/enums/dish.enum';
import { CuisineType } from 'src/app/models/enums/cuisine.enum';
import { RecipeType } from 'src/app/models/enums/recipe.enum';
import RecipeDisplayDto from 'src/app/dtos/recipe-display.dto';
import RecipeCategory from 'src/app/models/recipe-category.model';
import Category from 'src/app/models/category.model';

@Component({
  selector: 'app-edit-recipe',
  templateUrl: './edit-recipe.component.html',
  styleUrls: ['./edit-recipe.component.scss']
})
export class EditRecipeComponent implements OnInit {

  dishTypes = Object.keys(DishType).filter((item) => {
    return isNaN(Number(item));
  });
  cuisineTypes = Object.keys(CuisineType).filter((item) => {
    return isNaN(Number(item));
  });
  recipeTypes = Object.keys(RecipeType).filter((item) => {
    return isNaN(Number(item));
  });
  displayRecipe!: Recipe;
  tempCategories: number[] = [];
  categoriesToDisplay: string[] = [];
  categoryId!: number;
  private editRecipeSubscription?: Subscription;


  constructor(
    private _recipeService: RecipeService,
    private _notificationService: NotificationService,
    private _dialogRef: MatDialogRef<EditRecipeComponent>,
    @Inject(MAT_DIALOG_DATA) data: any,
  ) {

    this.displayRecipe = { ...data };
  }

  ngOnInit() {
    this.loadCategoriesToEditRecipeForm();
  }

  ngOnDestroy() {
    this.editRecipeSubscription?.unsubscribe();
  }

  onEditRecipeSubmit(recipe: Recipe) {
    recipe.recipeCategories = [];
    this.tempCategories.forEach(category => {
      recipe.recipeCategories.push(new RecipeCategory(new Category(category)))
    });

    this.editRecipeSubscription = this._recipeService.updateRecipe(recipe)
      .subscribe({
        next: () => {
          this._notificationService.openSnackBar("Recipe updated!", "Done");
          this._dialogRef.close(true);
        },
        error: (res) => {
          console.log(res);
          this._notificationService.openSnackBar("There was a problem updating recipe.", "Close");
        },
      });
  }
  addRecipeCatToIput() {
    if (this.tempCategories?.indexOf(this.categoryId) === -1 && this.categoryId > 0) { // check if value is unique
      this.tempCategories?.push(this.categoryId);
      this.categoriesToDisplay = [...this.categoriesToDisplay, RecipeType[this.categoryId]]; // push is not detected as a change in ngModel
      console.log(this.tempCategories);
      console.log(this.categoriesToDisplay);
    }
  }

  removeRecipeCatFromInput() {
    this.tempCategories = this.tempCategories?.filter((item) => item != this.categoryId);
    this.categoriesToDisplay = this.categoriesToDisplay.filter((item) => item != RecipeType[this.categoryId]);
    console.log(this.tempCategories);
    console.log(this.categoriesToDisplay);
  }

  loadCategoriesToEditRecipeForm(){
    this.displayRecipe.recipeCategories.forEach(element => {
      if(element.category != null){
        this.tempCategories.push(element.category.recipeType);
        this.categoriesToDisplay.push(RecipeType[element.category.recipeType]);
      }
    });
  }

}
