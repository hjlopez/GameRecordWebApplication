import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BilliardsMatch } from '../_models/billiards/BilliardsMatch';
import { BilliardsTournament } from '../_models/billiards/BilliardsTournament';
import { BilliardsTournamentMembers } from '../_models/billiards/BilliardsTournamentMembers';
import { TournamentModes } from '../_models/billiards/TournamentModes';

@Injectable({
  providedIn: 'root'
})
export class BilliardsMatchService {

  constructor(private http: HttpClient) { }

  getTournaments(userId: number): Observable<BilliardsTournament[]>
  {
    return this.http.get<BilliardsTournament[]>(environment.apiUrl + 'billiards/get-billiards-tournament/' + userId).pipe(
      map((response: BilliardsTournament[]) => {
        return response;
      })
    );
  }


  getMatches(): Observable<BilliardsMatch[]>
  {
    return this.http.get<BilliardsMatch[]>(environment.apiUrl + 'billiardsGame/get-matches').pipe(
      map((response: BilliardsMatch[]) => {
        return response;
      })
    );
  }

  getMatch(id: number): Observable<BilliardsMatch>
  {
    return this.http.get<BilliardsMatch>(environment.apiUrl + 'billiardsGame/get-match/' + id).pipe(
      map((response: BilliardsMatch) => {
        return response;
      })
    );
  }

  getMatchUser(userId: number): Observable<BilliardsMatch[]>
  {
    return this.http.get<BilliardsMatch[]>(environment.apiUrl + 'billiardsGame/get-match-user/' + userId).pipe(
      map((response: BilliardsMatch[]) => {
        return response;
      })
    );
  }

  getMatchType(typeId: number): Observable<BilliardsMatch[]>
  {
    return this.http.get<BilliardsMatch[]>(environment.apiUrl + 'billiardsGame/get-match-type/' + typeId).pipe(
      map((response: BilliardsMatch[]) => {
        return response;
      })
    );
  }

  getMatchMode(modeId: number): Observable<BilliardsMatch[]>
  {
    return this.http.get<BilliardsMatch[]>(environment.apiUrl + 'billiardsGame/get-match-mode/' + modeId).pipe(
      map((response: BilliardsMatch[]) => {
        return response;
      })
    );
  }

  getMatchSeason(seasonId: number): Observable<BilliardsMatch[]>
  {
    return this.http.get<BilliardsMatch[]>(environment.apiUrl + 'billiardsGame/get-match-season/' + seasonId).pipe(
      map((response: BilliardsMatch[]) => {
        return response;
      })
    );
  }

  getMatchTournament(tournamentId: number): Observable<BilliardsMatch[]>
  {
    return this.http.get<BilliardsMatch[]>(environment.apiUrl + 'billiardsGame/get-match-tournament/' + tournamentId).pipe(
      map((response: BilliardsMatch[]) => {
        return response;
      })
    );
  }

  insertMatch(match: BilliardsMatch[]): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'billiardsGame/insert-match', match).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  updateMatch(match: BilliardsMatch[]): Observable<any>
  {
    return this.http.put(environment.apiUrl + 'billiardsGame/update-match', match).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteMatch(id: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'billiardsGame/delete-match/' + id).pipe(
      map((response: any) => {
        return response;
      })
    );
  }
}
