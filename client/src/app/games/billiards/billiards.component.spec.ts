import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BilliardsComponent } from './billiards.component';

describe('BilliardsComponent', () => {
  let component: BilliardsComponent;
  let fixture: ComponentFixture<BilliardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BilliardsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BilliardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
