import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecipesListComponent } from './components/features/recipe/recipes-list/recipes-list.component';
import { AddRecipeComponent } from './components/features/recipe/add-recipe/add-recipe.component';
import { EditRecipeComponent } from './components/features/recipe/edit-recipe/edit-recipe.component';

const routes: Routes = [
  {
    path: "",
    component: RecipesListComponent
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
