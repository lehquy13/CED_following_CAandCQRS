import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TutorDetailComponent } from './tutor-detail.component';

describe('TutorDetailComponent', () => {
  let component: TutorDetailComponent;
  let fixture: ComponentFixture<TutorDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TutorDetailComponent]
    });
    fixture = TestBed.createComponent(TutorDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
