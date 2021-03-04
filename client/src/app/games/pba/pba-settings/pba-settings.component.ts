import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Schedule } from 'src/app/_models/pba/Schedule';
import { ScheduleGroup } from 'src/app/_models/pba/ScheduleGroup';
import { ScheduleService } from 'src/app/_services/pba/schedule.service';

@Component({
  selector: 'app-pba-settings',
  templateUrl: './pba-settings.component.html',
  styleUrls: ['./pba-settings.component.css']
})
export class PbaSettingsComponent implements OnInit {
  schedGroup: ScheduleGroup[] = [];
  sched: Schedule[] = [];

  constructor(private modalService: BsModalService, private toastr: ToastrService, private schedule: ScheduleService) { }

  ngOnInit(): void {
    this.schedule.getScheduleGroups().subscribe(s => {
      this.schedGroup = s;
    });
  }

  loadSchedule(groupId: any): void
  {
    this.schedule.getSelectedGroup(Number(groupId)).subscribe(s => {
      this.sched = s;
    });
  }

}
