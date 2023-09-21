import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecipesListComponent } from './components/recipe/recipes-list/recipes-list.component';
import { AddRecipeComponent } from './components/recipe/add-recipe/add-recipe.component';
import { EditRecipeComponent } from './components/recipe/edit-recipe/edit-recipe.component';

const routes: Routes = [
  {
    path: "",
    component: RecipesListComponent
  },
  {
    path: "recipes",
    component: RecipesListComponent
  },  
  {
    path: "recipes/add",
    component: AddRecipeComponent
  }
  ,  
  {
    path: "recipes/edit/:id",
    component: EditRecipeComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
