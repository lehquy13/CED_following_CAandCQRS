import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileAllLearningClassComponent } from './profile-all-learning-class.component';

describe('ProfileAllLearningClassComponent', () => {
  let component: ProfileAllLearningClassComponent;
  let fixture: ComponentFixture<ProfileAllLearningClassComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProfileAllLearningClassComponent]
    });
    fixture = TestBed.createComponent(ProfileAllLearningClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
