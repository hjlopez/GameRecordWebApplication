import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndividualStatsComponent } from './individual-stats.component';

describe('IndividualStatsComponent', () => {
  let component: IndividualStatsComponent;
  let fixture: ComponentFixture<IndividualStatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndividualStatsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndividualStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
