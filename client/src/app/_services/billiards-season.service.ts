import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Season } from '../_models/billiards/Season';
import { SeasonHistory } from '../_models/billiards/SeasonHistory';

@Injectable({
  providedIn: 'root'
})
export class BilliardsSeasonService {

  constructor(private http: HttpClient) { }

  getTournamentSeasons(tournamentId: number): Observable<Season[]>
  {
    return this.http.get<Season[]>(environment.apiUrl + 'billiards/get-tournament-seasons/' + tournamentId).pipe(
      map((response: Season[]) => {
        return response;
      })
    );
  }

  insertTournamentSeason(season: Season): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'billiards/insert-tournament-season', season).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteTournamentSeason(seasonNumberId: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'billiards/delete-tournament-season/' + seasonNumberId).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  updateTournamentSeason(season: Season): Observable<any>
  {
    return this.http.put(environment.apiUrl + 'billiards/update-tournament-season', season).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  insertSeasonHistory(seasonHistory: Partial<SeasonHistory>): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'seasonHistory/insert-history', seasonHistory).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteSeasonHistory(matchId: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'seasonHistory/delete-history/' + matchId).pipe(
      map((response: any) => {
        return response;
      })
    );
  }
}
