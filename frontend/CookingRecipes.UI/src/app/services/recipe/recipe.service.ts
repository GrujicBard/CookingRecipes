import { Injectable } from '@angular/core';
import Recipe from '../../models/recipe';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import RecipeDisplayDto from 'src/app/dtos/recipeDisplayDto';

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

  public getRecipesByTitle(title: string): Observable<Recipe[]> {
    return this.httpClient.get<Recipe[]>(`${this.baseApiUrl}/${this.url}/title/${title}`);
  };

  public addRecipe(recipe: Recipe): Observable<string> {
    return this.httpClient.post<string>(`${this.baseApiUrl}/${this.url}`, recipe, { responseType: 'text' as 'json' });
  };

  public updateRecipe(recipe: RecipeDisplayDto): Observable<void> {
    return this.httpClient.put<void>(`${this.baseApiUrl}/${this.url}/${recipe.id}`, recipe);
  };

  public deleteRecipe(recipe: Recipe): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseApiUrl}/${this.url}/${recipe.id}`);
  };
}

