import { MatDialogRef } from '@angular/material/dialog';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import Recipe from 'src/app/models/recipe.model';
import { RecipeService } from 'src/app/services/recipe/recipe.service';
import { NotificationService } from 'src/app/services/core/notifications/notification.service';
import { DishType } from 'src/app/models/enums/dish.enum';
import { CuisineType } from 'src/app/models/enums/cuisine.enum';
import { RecipeType } from 'src/app/models/enums/recipe.enum';
import RecipeCategory from 'src/app/models/recipe-category.model';
import Category from 'src/app/models/category.model';


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
  recipeTypes = Object.keys(RecipeType).filter((item) => {
    return isNaN(Number(item));
  });

  createRecipe!: Recipe;
  categoryId: number;
  categoriesToDisplay: string[] = [];
  tempCategories: number[] = [];
  private _addRecipeSubscription?: Subscription;

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
      recipeCategories: [],
    };
    this.categoryId = 0;
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this._addRecipeSubscription?.unsubscribe();
  }

  onAddRecipeSubmit(recipe : Recipe) {
    this.tempCategories.forEach(category => {
      recipe.recipeCategories.push(new RecipeCategory(new Category(category)))
    });
    this._addRecipeSubscription = this._recipeService.addRecipe(recipe)
      .subscribe({
        next: (res) => {
          this._notificationService.openSnackBar(res, "Done");
          this._dialogRef.close(true);
        },
        error: (res) => {
          this._notificationService.openSnackBar("There was a problem adding recipe.", "Close");
          console.log(res);
        },
      });
  }

  addRecipeCatToIput() {
    if (this.tempCategories?.indexOf(this.categoryId) === -1) { // check if value is unique
      this.tempCategories?.push(this.categoryId); // enums start with 0, ids start with 1
      this.categoriesToDisplay = [...this.categoriesToDisplay, RecipeType[this.categoryId]]; // push is not detected as a change in ngModel
    }
  }

  removeRecipeCatFromInput() {
    this.tempCategories = this.tempCategories?.filter((item) => item != this.categoryId);
    this.categoriesToDisplay = this.categoriesToDisplay.filter((item) => item != RecipeType[this.categoryId]);
  }
}
