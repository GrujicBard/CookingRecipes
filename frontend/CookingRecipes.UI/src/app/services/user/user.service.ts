import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import LoginDto from 'src/app/dtos/login.dto';
import User from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseApiUrl: string = environment.baseApiUrl
  private url = "User";

  constructor(private httpClient: HttpClient) { }

  public register(user: User): Observable<string> {
    return this.httpClient.post<string>(`${this.baseApiUrl}/${this.url}/register`, user, { responseType: 'text' as 'json' });
  };

  public login(user: LoginDto): Observable<string> {
    return this.httpClient.post<string>(`${this.baseApiUrl}/${this.url}/login`, user, { responseType: 'text' as 'json' });
  };
}
