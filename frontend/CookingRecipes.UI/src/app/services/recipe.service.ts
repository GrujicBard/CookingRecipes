import { Injectable } from '@angular/core';
import Recipe from '../models/recipe';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  baseApiUrl: string = environment.baseApiUrl
  private url = "Recipe";

  constructor(private httpClient: HttpClient) {
  }

  public getRecipes(): Observable<Recipe[]> {
    return this.httpClient.get<Recipe[]>(`${this.baseApiUrl}/${this.url}`);
  };

  public getRecipeById(recipeId: number): Observable<Recipe> {
    return this.httpClient.get<Recipe>(`${this.baseApiUrl}/${this.url}/${recipeId}`);
  };

  public postRecipe(categoryId: number, recipe: Recipe): Observable<Recipe> {
    return this.httpClient.post<Recipe>(`${this.baseApiUrl}/${this.url}?categoryId=${categoryId}`, recipe);
  };

  public updateRecipe(recipe: Recipe): Observable<Recipe> {
    return this.httpClient.put<Recipe>(`${this.baseApiUrl}/${this.url}/${recipe.id}`, recipe);
  };

  public deleteRecipe(recipe: Recipe): Observable<Recipe> {
    return this.httpClient.delete<Recipe>(`${this.baseApiUrl}/${this.url}/${recipe.id}`);
  };

}

