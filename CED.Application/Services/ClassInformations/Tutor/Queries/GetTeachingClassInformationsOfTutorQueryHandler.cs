using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Tutor.Queries;

public class GetTeachingClassInformationsOfTutorQueryHandler : GetAllQueryHandler<GetTeachingClassInformationsOfTutorQuery,ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    public GetTeachingClassInformationsOfTutorQueryHandler(IClassInformationRepository classInformationRepository, IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<List<ClassInformationDto>> Handle(GetTeachingClassInformationsOfTutorQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var classInformations =  _classInformationRepository.GetTeachingClassInformationsByUserId(query.Guid);

            return _mapper.Map<List<ClassInformationDto>>(classInformations);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

