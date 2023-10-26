import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import Recipe from 'src/app/models/recipe.model';
import { RecipeService } from 'src/app/services/recipe/recipe.service';
import { AddRecipeComponent } from '../add-recipe/add-recipe.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { EditRecipeComponent } from '../edit-recipe/edit-recipe.component';
import { NotificationService } from 'src/app/services/core/notifications/notification.service';
import { DialogService } from 'src/app/services/core/dialogs/dialog.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { DishType } from 'src/app/models/enums/dishType';
import { CuisineType } from 'src/app/models/enums/cuisineType';
import { RecipeType } from 'src/app/models/enums/recipeType';
import RecipeDisplayDto from 'src/app/dtos/recipe-display.dto';

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],

})
export class RecipesListComponent implements OnInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  recipesDataSource!: MatTableDataSource<Recipe>;
  columnsToDisplay = ['id', 'title', 'recipeType', 'dishType', 'cuisineType', 'difficulty', 'actions'];
  expandedElement!: Recipe | null;

  recipes!: Recipe[];

  tableDef: Array<any> = [
    {
      key: 'id',
      header: 'Id',
      className: 'number'
    },
    {
      key: 'title',
      header: 'Title',
      className: 'string'
    },
    {
      key: 'recipeType',
      header: 'Categories',
      className: 'string'
    },
    {
      key: 'dishType',
      header: 'Dish Type',
      className: 'number'
    },
    {
      key: 'cuisineType',
      header: 'Cuisine Type',
      className: 'number'
    },
    {
      key: 'difficulty',
      header: 'Difficulty',
      className: 'number'
    },
  ]

  constructor(
    private _recipeService: RecipeService,
    private _notificationService: NotificationService,
    private _dialog: MatDialog,
    private _dialogService: DialogService,
  ) { }

  ngOnInit() {
    this.getRecipes();
  }

  openAddRecipeDialog() {
    const addDialogRef = this._dialog.open(AddRecipeComponent);
    addDialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getRecipes();
        }
      },
    });
  }

  openEditRecipeDialog(data: any) {
    const dialogRef = this._dialog.open(EditRecipeComponent, {
      data,
      autoFocus: false
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getRecipes();
        }
      },
    });
  }

  applyFilterToRecipes(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.recipesDataSource.filter = filterValue;

    if (this.recipesDataSource.paginator) {
      this.recipesDataSource.paginator.firstPage();
    }
  }

  getRecipes() {
    this._recipeService.getRecipes()
      .subscribe({
        next: (recipes) => {
          /* Manage data to display in recipes list */
          this.recipes = recipes;
          this.recipesDataSource = new MatTableDataSource(recipes);
          setTimeout(() => {
            this.recipesDataSource.sort = this.sort
          });
          this.recipesDataSource.paginator = this.paginator;
          this.recipesDataSource.filterPredicate = function (data: RecipeDisplayDto, filter: string): boolean {
            return data.title.toLowerCase().includes(filter);
          };
        },
        error: (response) => {
          console.log(response);
        },
      });
  }

  openDeleteRecipeDialog(recipe: Recipe) {
    this._dialogService.confirmDialog({
      title: "Delete recipe",
      message: "Are you use you want to delete recipe?",
      confirmText: "Delete",
      cancelText: "Cancel",
    }).subscribe(data => {
      if (data) {
        this._recipeService.deleteRecipe(recipe).subscribe({
          next: () => {
            this._notificationService.openSnackBar("Recipe deleted!", "Done");
            this.getRecipes();
          },
          error: (res) => {
            this._notificationService.openSnackBar("There was a problem deleting recipe.", "Close");
            console.log(res);

          }
        });
      }
    })
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

}
