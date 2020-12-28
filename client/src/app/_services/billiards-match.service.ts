import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BilliardsTournament } from '../_models/billiards/BilliardsTournament';

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
}
