import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavLoggedComponent } from './nav-logged.component';

describe('NavLoggedComponent', () => {
  let component: NavLoggedComponent;
  let fixture: ComponentFixture<NavLoggedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavLoggedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavLoggedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
