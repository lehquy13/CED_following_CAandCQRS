import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TutorCardComponent } from './tutor-card.component';

describe('TutorCardComponent', () => {
  let component: TutorCardComponent;
  let fixture: ComponentFixture<TutorCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TutorCardComponent]
    });
    fixture = TestBed.createComponent(TutorCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
