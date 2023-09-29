import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RecipesListComponent } from './components/features/recipe/recipes-list/recipes-list.component';
import { AddRecipeComponent } from './components/features/recipe/add-recipe/add-recipe.component';
import { EditRecipeComponent } from './components/features/recipe/edit-recipe/edit-recipe.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './components/core/navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    RecipesListComponent,
    AddRecipeComponent,
    EditRecipeComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
