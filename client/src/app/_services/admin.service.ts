import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BilliardsTournament } from '../_models/BilliardsTournament';
import { BilliardsTournamentMembers } from '../_models/BilliardsTournamentMembers';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<Partial<User[]>>
  {
    return this.http.get<Partial<User[]>>(environment.apiUrl + 'admin/get-users').pipe(
      map((response: Partial<User[]>) => {
        const user = response;
        return user;
      })
    );
  }

  deleteUser(username: string): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'admin/delete-user/' + username).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  updateUser(user: User): Observable<any>
  {
    return this.http.put(environment.apiUrl + 'admin/update-user', user).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  // billiards
  getTournaments(): Observable<BilliardsTournament[]>
  {
    return this.http.get<BilliardsTournament[]>(environment.apiUrl + 'admin/get-billiards-tournament').pipe(
      map((response: BilliardsTournament[]) => {
        return response;
      })
    );
  }

  addTournament(tournament: BilliardsTournament): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'admin/insert-billiards-tournament', tournament).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  editTournament(tournament: BilliardsTournament): Observable<any>
  {
    return this.http.put(environment.apiUrl + 'admin/update-billiards-tournament', tournament).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  deleteTournament(tournamentId: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'admin/delete-billiards-tournament/' + tournamentId).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  getMembers(tournamentId: number): Observable<BilliardsTournamentMembers[]>
  {
    return this.http.get<BilliardsTournamentMembers[]>(environment.apiUrl + 'admin/get-tournament-members/' + tournamentId).pipe(
      map((response: BilliardsTournamentMembers[]) => {
        return response;
      })
    );
  }

  searchMember(knownAs: string): Observable<User>
  {
    return this.http.get<User>(environment.apiUrl + 'admin/get-user/' + knownAs).pipe(
      map((response: User) => {
        return response;
      })
    );
  }

  addMember(user: BilliardsTournamentMembers): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'admin/insert-tournament-members', user).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  removeMember(id: number): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'admin/delete-tournament-members/' + id).pipe(
      map((response: any) => {
        return response;
      })
    );
  }
}
