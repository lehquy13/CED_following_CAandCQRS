using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.TutorReview;
using CED.Domain.ClassInformations;
using CED.Domain.Review;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;

namespace CED.Application.Mapping;

public class ClassInformationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Config for Request getting class
        config.NewConfig<RequestGettingClass, RequestGettingClassMinimalDto>();
        config.NewConfig<RequestGettingClassMinimalDto, RequestGettingClass>();
        //Config for Tutor review
        config.NewConfig<TutorReview, TutorReviewDto>();
       
        
        //Config for Class Information
        config.NewConfig<ClassInformationDto, ClassInformation>()        
            .Map(dest => dest.TutorId, src => src.TutorId)
            .Map(dest => dest, src => src);
        config.NewConfig<ClassInformation, ClassInformationDto >()        
            .Map(dest => dest.TutorId, src => src.TutorId)
            .Map(dest => dest, src => src);
        config.NewConfig<ClassInformation, ClassInformationForEditDto>()
            .Map(dest => dest.TutorId, src => src.TutorId)
            .Map(dest => dest.TutorName, src => src.Tutor!.GetFullNAme() ?? "", srcCond => srcCond.Tutor != null)
            .Map(dest => dest.LearnerName, src => src.Learner!.GetFullNAme() ?? "", srcCond => srcCond.Learner != null)
            .Map(dest => dest, src => src);
         config.NewConfig<ClassInformation, ClassInformationForDetailDto>()
            .Map(dest => dest.TutorId, src => src.TutorId)
            .Map(dest => dest.TutorName, src => src.Tutor!.GetFullNAme() ?? "", srcCond => srcCond.Tutor != null)
            .Map(dest => dest.LearnerName, src => src.Learner!.GetFullNAme() ?? "", srcCond => srcCond.Learner != null)
            .Map(dest => dest.TutorReviewDto, src => src.TutorReviews.Description)
            .Map(dest => dest.TutorReviewDtoId, src => src.TutorReviews.Id)
            
            .Map(dest => dest, src => src);
        
        
        config.NewConfig<ClassInformation, ClassInformationForListDto >()        
           
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.LearningMode, src => src.LearningMode.ToString())
            .Map(dest => dest.GenderRequirement, src => src.GenderRequirement.ToString())
            .Map(dest => dest.AcademicLevel, src => src.AcademicLevelRequirement.ToString())
            .Map(dest => dest.LearnerGender, src => src.LearnerGender.ToString())
            .Map(dest => dest, src => src);

        //Config for TutorReview
        config.NewConfig<TutorReviewDto, TutorReview>();

        config.NewConfig<(RequestGettingClass, ClassInformation,string), RequestGettingClassDto>()
            .Map(dest => dest.Title, src => src.Item2.Title)
            .Map(dest => dest.SubjectName, src => src.Item3)
            .Map(dest => dest, src => src.Item1);
        // config.NewConfig<ClassInformation, RequestGettingClassExtendDto>()
        //     .Map(dest => dest.ContactNumber, src => src.ContactNumber)
        //     .Map(dest => dest.LearnerName, src => src.LearnerName)
        //     .Map(dest => dest.Title, src => src.Title)
        //     .Map(dest => dest.TutorId, src => src.TutorId)
        //     .Map(dest => dest.RequestStatus, src => src.RequestGettingClasses.)
        //     //.Map(dest => dest.Tutor, src => src.Item3)
        //     .Map(dest => dest.SubjectName, src => src.Subject.Name)
        //     .Map(dest => dest, src => src);
        config.NewConfig<RequestGettingClass, RequestGettingClassExtendDto>()
            .Map(dest => dest.ContactNumber, src => src.ClassInformation.ContactNumber)
            .Map(dest => dest.LearnerName, src => src.ClassInformation.LearnerName)
            .Map(dest => dest.Title, src => src.ClassInformation.Title)
            .Map(dest => dest.TutorId, src => src.ClassInformation.Id)
            //.Map(dest => dest.Tutor, src => src.Item3)
            .Map(dest => dest.SubjectName, src => src.ClassInformation.Subject.Name)
            .Map(dest => dest, src => src);
        config.NewConfig<(RequestGettingClass, User), RequestGettingClassMinimalDto>()
            .Map(dest => dest.Id, src => src.Item1.Id)
            .Map(dest => dest.ClassInformationId, src => src.Item1.ClassInformationId)
            .Map(dest => dest.Description, src => src.Item1.Description)
            .Map(dest => dest.RequestStatus, src => src.Item1.RequestStatus)
            .Map(dest => dest.TutorId, src => src.Item2.Id)
            .Map(dest => dest.TutorName, src => $"{src.Item2.FirstName} " + $"{src.Item2.LastName}")
            .Map(dest => dest.TutorPhoneNumber, src => src.Item2.PhoneNumber)
            .Map(dest => dest.TutorEmail, src => src.Item2.Email);


        config.NewConfig<Subject, SubjectLookupDto>();


    }
}

