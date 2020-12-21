import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TournamentModalComponent } from './tournament-modal.component';

describe('TournamentModalComponent', () => {
  let component: TournamentModalComponent;
  let fixture: ComponentFixture<TournamentModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TournamentModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TournamentModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
