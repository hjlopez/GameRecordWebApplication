import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.css']
})
export class GeneralSettingsComponent implements OnInit {
  addGameTag = false;
  addGameTypesTag = false;

  constructor() { }

  ngOnInit(): void {
  }

}
