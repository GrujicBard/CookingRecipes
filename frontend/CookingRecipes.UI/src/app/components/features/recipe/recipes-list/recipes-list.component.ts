import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import Recipe from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe/recipe.service';
import { AddRecipeComponent } from '../add-recipe/add-recipe.component';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { DialogRef } from '@angular/cdk/dialog';
import { EditRecipeComponent } from '../edit-recipe/edit-recipe.component';
import { NotificationService } from 'src/app/services/core/notifications/notification.service';

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.scss']
})
export class RecipesListComponent implements OnInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  recipes!: MatTableDataSource<Recipe>;
  displayedColumns = ['Id', 'Title', 'Dish Type', 'Image Name', 'Difficulty'];

  columns = [
    {
      columnDef: 'Id',
      header: 'Id',
      cell: (recipe: Recipe) => `${recipe.id}`,
    },
    {
      columnDef: 'Title',
      header: 'Title',
      cell: (recipe: Recipe) => `${recipe.title}`,
    },
    {
      columnDef: 'Dish Type',
      header: 'Dish Type',
      cell: (recipe: Recipe) => `${recipe.dishType}`,
    },
    {
      columnDef: 'Image Name',
      header: 'Image Name',
      cell: (recipe: Recipe) => `${recipe.imageName}`,
    },
    {
      columnDef: 'Difficulty',
      header: 'Difficulty',
      cell: (recipe: Recipe) => `${recipe.difficulty}`,
    },
  ];

  constructor(
    private _recipeService: RecipeService,
    private _notificationService: NotificationService,
    private _dialog: MatDialog,

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
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getRecipes();
        }
      },
    });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.recipes.filter = filterValue;

    if (this.recipes.paginator) {
      this.recipes.paginator.firstPage();
    }
  }
  getRecipes() {
    this._recipeService.getRecipes()
      .subscribe({
        next: (recipes) => {
          this.recipes = new MatTableDataSource(recipes);
          this.recipes.sort = this.sort;
          this.recipes.paginator = this.paginator;
          this.recipes.filterPredicate = function (data: Recipe, filter: string): boolean {
            return data.title.toLowerCase().includes(filter);
          };
        },
        error: (response) => {
          console.log(response);
        },
      });
  }

  deleteRecipe(recipe: Recipe) {
    this._recipeService.deleteRecipe(recipe).subscribe({
      next: () => {
        this._notificationService.openSnackBar("Recipe deleted!", "Done");
        this.getRecipes();
      },
      error: (res) => {
        this._notificationService.openSnackBar("There was a problem deleting recipe.", "Close");
        console.log(res);

      }
    })
  }

}
