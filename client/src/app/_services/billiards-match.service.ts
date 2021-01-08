import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BilliardsMatch } from '../_models/billiards/BilliardsMatch';
import { BilliardsTournament } from '../_models/billiards/BilliardsTournament';
import { BilliardsTournamentMembers } from '../_models/billiards/BilliardsTournamentMembers';
import { MatchParams } from '../_models/billiards/MatchParams';
import { PaginatedResult } from '../_models/billiards/Pagination';
import { TournamentModes } from '../_models/billiards/TournamentModes';
import { UserWins } from '../_models/billiards/UserWins';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

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

  getMatchTournament(matchParams: MatchParams): Observable<PaginatedResult<BilliardsMatch[]>>
  {
    let params = getPaginationHeaders(matchParams.pageNumber, matchParams.pageSize);
    params = params.append('tournamentId', matchParams.tournamentId.toString());

    return getPaginatedResult<PaginatedResult<BilliardsMatch[]>>(environment.apiUrl + 'billiardsGame/get-match-tournament',
        params , this.http).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  getFilteredMatch(matchParams: MatchParams): Observable<PaginatedResult<BilliardsMatch[]>>
  {
    let params = getPaginationHeaders(matchParams.pageNumber, matchParams.pageSize);
    params = params.append('tournamentId', matchParams.tournamentId.toString());
    params = params.append('typeId', matchParams.typeId.toString());
    params = params.append('modeId', matchParams.modeId.toString());
    params = params.append('seasonNumberId', matchParams.seasonNumberId.toString());

    return getPaginatedResult<PaginatedResult<BilliardsMatch[]>>(environment.apiUrl + 'billiardsGame/get-filtered-match',
        params , this.http).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  insertMatch(match: BilliardsMatch): Observable<any>
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

  getSeasonTournamentMatchUp(wins: UserWins): Observable<UserWins>
  {
    let param = new HttpParams();
    param = param.append('userId', wins.userId.toString());
    param = param.append('opponentUserId', wins.opponentUserId.toString());
    param = param.append('typeId', wins.typeId.toString());
    param = param.append('seasonNumberId', wins.seasonNumberId.toString());
    param = param.append('tournamentId', wins.tournamentId.toString());

    return this.http.get(environment.apiUrl + 'userWins/get-season-type', {params: param}).pipe(
      map((response: any) => {
        return response;
      })
    );

  }

  getSeasonMatchUp(wins: UserWins): Observable<UserWins>
  {
    let param = new HttpParams();
    param = param.append('userId', wins.userId.toString());
    param = param.append('opponentUserId', wins.opponentUserId.toString());
    param = param.append('seasonNumberId', wins.seasonNumberId.toString());
    param = param.append('tournamentId', wins.tournamentId.toString());

    return this.http.get(environment.apiUrl + 'userWins/get-season-matchup', {params: param}).pipe(
      map((response: any) => {
        return response;
      })
    );
  }
}
