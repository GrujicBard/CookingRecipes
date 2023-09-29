import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import Recipe from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe/recipe.service';
import { AddRecipeComponent } from '../add-recipe/add-recipe.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { EditRecipeComponent } from '../edit-recipe/edit-recipe.component';
import { NotificationService } from 'src/app/services/core/notifications/notification.service';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { DialogService } from 'src/app/services/core/dialogs/dialog.service';

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.scss']
})
export class RecipesListComponent implements OnInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  recipes!: MatTableDataSource<Recipe>;
  displayedColumns = ['id', 'title', 'dishType', 'imageName', 'difficulty'];

  columns = [
    {
      columnDef: 'id',
      header: 'id',
      cell: (recipe: Recipe) => `${recipe.id}`,
    },
    {
      columnDef: 'title',
      header: 'title',
      cell: (recipe: Recipe) => `${recipe.title}`,
    },
    {
      columnDef: 'dishType',
      header: 'dishType',
      cell: (recipe: Recipe) => `${recipe.dishType}`,
    },
    {
      columnDef: 'imageName',
      header: 'imageName',
      cell: (recipe: Recipe) => `${recipe.imageName}`,
    },
    {
      columnDef: 'difficulty',
      header: 'difficulty',
      cell: (recipe: Recipe) => `${recipe.difficulty}`,
    },
  ];

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
          setTimeout(() => {
            this.recipes.sort = this.sort
          });
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

}