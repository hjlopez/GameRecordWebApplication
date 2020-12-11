import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DummyMainComponent } from './dummy-main.component';

describe('DummyMainComponent', () => {
  let component: DummyMainComponent;
  let fixture: ComponentFixture<DummyMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DummyMainComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DummyMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
