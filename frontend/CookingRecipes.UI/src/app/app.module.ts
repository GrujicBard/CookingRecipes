import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'

/* Material */
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatListModule } from '@angular/material/list';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatMenuModule } from '@angular/material/menu';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RecipesListComponent } from './components/features/admin/recipe/recipes-list/recipes-list.component';
import { AddRecipeComponent } from './components/features/admin/recipe/add-recipe/add-recipe.component';
import { EditRecipeComponent } from './components/features/admin/recipe/edit-recipe/edit-recipe.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './components/core/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmDialogComponent } from './components/core/dialogs/confirm-dialog/confirm-dialog.component';
import { RecipesListUserComponent } from './components/features/user/recipe/recipes-list-user/recipes-list-user.component';
import { RecipeDetailsComponent } from './components/features/user/recipe/recipe-details/recipe-details.component';
import { RegisterComponent } from './components/features/auth/register/register.component';
import { LoginComponent } from './components/features/auth/login/login.component';




@NgModule({
  declarations: [
    AppComponent,
    RecipesListComponent,
    AddRecipeComponent,
    EditRecipeComponent,
    NavbarComponent,
    ConfirmDialogComponent,
    RecipesListUserComponent,
    RecipeDetailsComponent,
    RegisterComponent,
    LoginComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCardModule,
    MatInputModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    BrowserModule,
    MatTableModule,
    MatSortModule,
    MatSnackBarModule,
    MatListModule,
    MatGridListModule,
    MatButtonToggleModule,
    MatMenuModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
