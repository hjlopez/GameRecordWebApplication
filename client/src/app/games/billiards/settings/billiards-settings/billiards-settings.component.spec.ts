import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BilliardsSettingsComponent } from './billiards-settings.component';

describe('BilliardsSettingsComponent', () => {
  let component: BilliardsSettingsComponent;
  let fixture: ComponentFixture<BilliardsSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BilliardsSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BilliardsSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
