import {Subject} from "./Subject";
import {TutorVerificationInfoDto} from "./TutorVerificationInfoDto";

export class Tutor {
  id: number;
  firstName: string;
  lastName: string;
  gender: string;
  birthYear: number;
  address: string;
  description: string;
  image: string;
  email: string;
  phoneNumber: string;
  role: string;

  //Tutor
  university: string;
  academicLevel: string;
  rate: number;
  isVerified: number;

  majors: Subject[] = [];
  tutorVerificationInfoDtos: TutorVerificationInfoDto[] = [];
}
