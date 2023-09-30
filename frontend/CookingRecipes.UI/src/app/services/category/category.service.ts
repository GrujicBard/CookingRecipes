import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import Category from 'src/app/models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseApiUrl: string = environment.baseApiUrl
  private url = "Category";
constructor(private httpClient: HttpClient) { }
  public getCategories(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(`${this.baseApiUrl}/${this.url}`);
  };

}
