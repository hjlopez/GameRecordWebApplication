import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Games } from '../_models/Games';

@Injectable({
  providedIn: 'root'
})
export class GamesService {

  constructor(private http: HttpClient) { }

  getGames(): Observable<Games[]>
  {
    return this.http.get<Games[]>(environment.apiUrl + 'games').pipe(
      map((games: any) => {
        return games;
      })
    );
  }
}
