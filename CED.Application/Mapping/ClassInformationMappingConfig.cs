using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;

namespace CED.Application.Mapping;

public class ClassInformationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<ClassInformationDto, ClassInformation>()        
            .Map(dest => dest.TutorId, src => src.TutorDtoId)
            .Map(dest => dest, src => src);

        config.NewConfig<(ClassInformation, Subject, User), ClassInformationDto>()
            .Map(dest => dest.SubjectName, src => src.Item2.Name)
            .Map(dest => dest.SubjectId, src => src.Item2.Id)
            .Map(dest => dest.TutorDtoId, src => src.Item3.Id)
            .Map(dest => dest.TutorPhoneNumber, src => src.Item3.PhoneNumber)
            .Map(dest => dest.TutorEmail, src => src.Item3.Email)
            .Map(dest => dest.TutorName, src => $"{src.Item3.FirstName} {src.Item3.LastName}")
            .Map(dest => dest, src => src.Item1);
        config.NewConfig<(ClassInformation, Subject), ClassInformationDto>() // in case the class doesnt have tutor
            .Map(dest => dest.SubjectName, src => src.Item2.Name)
            .Map(dest => dest.SubjectId, src => src.Item2.Id)
            .Map(dest => dest, src => src.Item1);


        config.NewConfig<Subject, SubjectLookupDto>();


    }
}

