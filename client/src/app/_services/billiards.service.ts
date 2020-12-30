import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BilliardsTournament } from '../_models/billiards/BilliardsTournament';
import { BilliardsTournamentMembers } from '../_models/billiards/BilliardsTournamentMembers';
import { MatchType } from '../_models/billiards/MatchType';
import { Modes } from '../_models/billiards/Modes';
import { TournamentMatchType } from '../_models/billiards/TournamentMatchType';
import { TournamentModes } from '../_models/billiards/TournamentModes';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class BilliardsService {

  constructor(private http: HttpClient) { }

  getTournaments(): Observable<BilliardsTournament[]>
  {
    return this.http.get<BilliardsTournament[]>(environment.apiUrl + 'billiardsSettings/get-billiards-tournament').pipe(
      map((response: BilliardsTournament[]) => {
        return response;
      })
    );
  }

  getTournamentsMembers(): Observable<BilliardsTournamentMembers[]>
  {
    return this.http.get<BilliardsTournamentMembers[]>(environment.apiUrl + 'billiardsSettings/get-billiards-tournament-member').pipe(
      map((response: BilliardsTournamentMembers[]) => {
        return response;
      })
    );
  }

  addTournament(tournament: BilliardsTournament): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'billiardsSettings/insert-billiards-tournament', tournament).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  editTournament(tournament: BilliardsTournament): Observable<any>
  {
    return this.http.put(environment.apiUrl + 'billiardsSettings/update-billiards-tournament', tournament).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteTournament(tournamentId: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'billiardsSettings/delete-billiards-tournament/' + tournamentId).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  getMembers(tournamentId: number): Observable<BilliardsTournamentMembers[]>
  {
    return this.http.get<BilliardsTournamentMembers[]>(environment.apiUrl
      + 'billiardsSettings/get-tournament-members/' + tournamentId).pipe(
      map((response: BilliardsTournamentMembers[]) => {
        return response;
      })
    );
  }

  searchMember(knownAs: string): Observable<User>
  {
    return this.http.get<User>(environment.apiUrl + 'billiardsSettings/get-user/' + knownAs).pipe(
      map((response: User) => {
        return response;
      })
    );
  }

  addMember(user: BilliardsTournamentMembers): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'billiardsSettings/insert-tournament-members', user).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  removeMember(id: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'billiardsSettings/delete-tournament-members/' + id).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  getTypes(): Observable<MatchType[]>
  {
    return this.http.get<MatchType[]>(environment.apiUrl + 'billiardsSettings/get-match-types').pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  getType(type: string): Observable<MatchType>
  {
    return this.http.get<MatchType>(environment.apiUrl + 'billiardsSettings/get-match-type/' + type).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  insertType(matchType: MatchType): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'billiardsSettings/insert-match-types', matchType).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  updateType(matchType: MatchType): Observable<any>
  {
    return this.http.put(environment.apiUrl + 'billiardsSettings/update-match-types', matchType).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteType(id: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'billiardsSettings/delete-match-types/' + id).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  getTournamentTypes(id: number): Observable<TournamentMatchType[]>
  {
    return this.http.get<TournamentMatchType[]>(environment.apiUrl + 'billiardsSettings/get-tournament-types/' + id).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  insertTournamentTypes(type: Partial<TournamentMatchType>): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'billiardsSettings/insert-tournament-types', type).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteTournamentType(id: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'billiardsSettings/delete-tournament-types/' + id).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  getModes(): Observable<Modes[]>
  {
    return this.http.get<Modes[]>(environment.apiUrl + 'billiardsSettings/get-modes').pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  insertMode(mode: Modes): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'billiardsSettings/insert-modes', mode).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  updateMode(mode: Modes): Observable<any>
  {
    return this.http.put(environment.apiUrl + 'billiardsSettings/update-modes', mode).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteMode(id: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'billiardsSettings/delete-modes/' + id).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  getTournamentModes(tournamentId: number): Observable<TournamentModes[]>
  {
    return this.http.get<TournamentModes[]>(environment.apiUrl + 'billiardsSettings/get-tournament-modes/' + tournamentId).pipe(
      map((response: any) => {
        return response;
      })
    );
  }


  getTournamentModesName(tournamentId: number): Observable<TournamentModes[]>
  {
    return this.http.get<TournamentModes[]>(environment.apiUrl + 'billiardsSettings/get-tournament-modes-name/' + tournamentId).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  insertTournamentModes(tournamentModes: TournamentModes): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'billiardsSettings/insert-tournament-modes', tournamentModes).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteTournamentModes(id: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'billiardsSettings/delete-tournament-modes/' + id).pipe(
      map((response: any) => {
        return response;
      })
    );
  }
}
