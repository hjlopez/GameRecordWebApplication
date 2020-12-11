import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dummy-main',
  templateUrl: './dummy-main.component.html',
  styleUrls: ['./dummy-main.component.css']
})
export class DummyMainComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.router.navigateByUrl('billiards');
  }

}
