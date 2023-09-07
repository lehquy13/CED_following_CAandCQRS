import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileCourseRequestComponent } from './profile-course-request.component';

describe('ProfileCourseRequestComponent', () => {
  let component: ProfileCourseRequestComponent;
  let fixture: ComponentFixture<ProfileCourseRequestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProfileCourseRequestComponent]
    });
    fixture = TestBed.createComponent(ProfileCourseRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
