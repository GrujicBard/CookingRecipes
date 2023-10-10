import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecipesListComponent } from './components/features/admin/recipe/recipes-list/recipes-list.component';
import { AddRecipeComponent } from './components/features/admin/recipe/add-recipe/add-recipe.component';
import { EditRecipeComponent } from './components/features/admin/recipe/edit-recipe/edit-recipe.component';
import { RecipesListUserComponent } from './components/features/user/recipe/recipes-list-user/recipes-list-user.component';

const routes: Routes = [
  {
    path: "",
    component: RecipesListUserComponent
  },
  {
    path: "recipes",
    component: RecipesListUserComponent
  },
  {
    path: "admin/recipes",
    component: RecipesListComponent
  },
  {
    path: "admin/recipes/add",
    component: AddRecipeComponent
  }
  ,
  {
    path: "admin/recipes/edit/:id",
    component: EditRecipeComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
