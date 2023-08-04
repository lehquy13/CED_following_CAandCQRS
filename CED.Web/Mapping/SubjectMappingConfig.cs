using CED.Application.Services.Subjects.Commands;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts.Subjects;
using CED.Web.Models;
using Mapster;

namespace CED.Web.Mapping;

public class SubjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Guid, GetSubjectQuery>()
            .Map(dest => dest.Id, src => src);
        config.NewConfig<Guid, DeleteSubjectCommand>()
            .Map(dest => dest.SubjectId, src => src);
        
        config.NewConfig<CreateUpdateSubjectDto, CreateUpdateSubjectCommand>()
            .Map(dest => dest.SubjectDto, src => src);


        //----------------------------------------------------------------
     

    }
}

