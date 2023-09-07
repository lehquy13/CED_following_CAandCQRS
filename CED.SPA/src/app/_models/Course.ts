import {Subject} from "./Subject";

export class Course {
  id: string;
  title: string;
  description: string;
  status: string;
  learningMode: string;
  fee: number;
  chargeFee: number;
  genderRequirement: string;
  learnerGender: string;
  academicLevel: string;
  numberOfLearner: number;
  minutePerSession: number;
  sessionPerWeek: number;
  subjectName: string;
  address: string;
  creationTime: Date;
}

export class LearningCourse {
  id: string;
  title: string;
  description: string;
  status: string;
  learningMode: string;
  fee: number;
  chargeFee: number;
  genderRequirement: string;
  learnerGender: string;
  academicLevel: string;
  numberOfLearner: number;
  minutePerSession: number;
  sessionPerWeek: number;
  subject: Subject;
  address: string;
  creationTime: Date;

  learnerName: string = "";
  contactNumber: string = "";
  learnerId: string

  //Confirmed data related
  tutorId: string;
  tutorName: string;
  tutorPhoneNumber: string;
  tutorEmail: string;

  tutorReviewDto: string;
  tutorReviewDtoId: string;
  rate : number;
}

export class TutorReview {
  id: string;
  description: string;
  learnerName: string;
  rate : number;
}
