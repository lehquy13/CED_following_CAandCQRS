import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileChangePasswordComponent } from './profile-change-password.component';

describe('ProfileChangePasswordComponent', () => {
  let component: ProfileChangePasswordComponent;
  let fixture: ComponentFixture<ProfileChangePasswordComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProfileChangePasswordComponent]
    });
    fixture = TestBed.createComponent(ProfileChangePasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
