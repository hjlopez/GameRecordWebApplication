import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Schedule } from 'src/app/_models/pba/Schedule';
import { ScheduleGroup } from 'src/app/_models/pba/ScheduleGroup';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  constructor(private http: HttpClient) { }

  getScheduleGroups(): Observable<ScheduleGroup[]>
  {
    return this.http.get<ScheduleGroup[]>(environment.apiUrl + 'schedule/get-groups').pipe(
      map((response: ScheduleGroup[]) => {
        return response;
      })
    );
  }

  getSelectedGroup(groupId: number): Observable<Schedule[]>
  {
    return this.http.get<Schedule[]>(environment.apiUrl + 'schedule/get-selected-sched/' + groupId).pipe(
      map((response: Schedule[]) => {
        return response;
      })
    );
  }
}
