import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileTutorEditComponent } from './profile-tutor-edit.component';

describe('ProfileTutorEditComponent', () => {
  let component: ProfileTutorEditComponent;
  let fixture: ComponentFixture<ProfileTutorEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProfileTutorEditComponent]
    });
    fixture = TestBed.createComponent(ProfileTutorEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
