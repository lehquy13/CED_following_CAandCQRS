using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using Mapster;

namespace CED.Application.Mapping;

public class ClassInformationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<ClassInformationDto, ClassInformation>()        
            .Map(dest => dest, src => src);

        config.NewConfig<(ClassInformation, Subject), ClassInformationDto>()
            .Map(dest => dest.SubjectName, src => src.Item2.Name)
            .Map(dest => dest.SubjectId, src => src.Item2.Id)
            .Map(dest => dest, src => src.Item1);


        config.NewConfig<Subject, SubjectLookupDto>();


    }
}

