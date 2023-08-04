﻿using CED.Domain.Common.Models;
using CED.Domain.Review;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;

namespace CED.Domain.ClassInformations;

public class ClassInformation : FullAuditedAggregateRoot<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.OnVerifying;
    public LearningMode LearningMode { get; set; } = LearningMode.Offline;

    public float Fee { get; set; } = 0;
    public float ChargeFee { get; set; } = 0;

    //Tutor related information

    public Gender GenderRequirement { get; set; } = Gender.None;
    public AcademicLevel AcademicLevelRequirement { get; set; } = AcademicLevel.Optional;

    //Student related information
    //public string StudentName { get; set; } = String.Empty;
    //public string StudentPhoneNumber { get; set; } = String.Empty;
    
    public string LearnerName { get; set; } = string.Empty;
    public Gender LearnerGender { get; set; } = Gender.Male;
    public int NumberOfLearner { get; set; } = 1;
    public string ContactNumber { get; set; } = string.Empty;
    public Guid? LearnerId { get; set; }
    public User? Learner { get; set; }


    // Time related information
    public int MinutePerSession { get; set; } = 90;
    public int SessionPerWeek { get; set; } = 2;

    // Address related information
    public string Address { get; set; } = string.Empty;

    //Subject related information
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; }
    public Guid? TutorId { get; set; }
    public Tutor? Tutor { get; set; }

    //Request of class
    public List<RequestGettingClass> RequestGettingClasses { get; set; } = new();
    public TutorReview TutorReviews { get; set; } = new();
}
