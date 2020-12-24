import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModesModalComponent } from './modes-modal.component';

describe('ModesModalComponent', () => {
  let component: ModesModalComponent;
  let fixture: ComponentFixture<ModesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModesModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
