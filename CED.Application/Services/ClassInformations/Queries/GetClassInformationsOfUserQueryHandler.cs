using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetClassInformationsOfUserQueryHandler : GetAllQueryHandler<GetClassInformationsOfUserQuery, ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    public GetClassInformationsOfUserQueryHandler(IClassInformationRepository classInformationRepository, IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<List<ClassInformationDto>> Handle(GetClassInformationsOfUserQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var classInformations = _classInformationRepository.GetLearningClassInformationsByUserId(query.Guid);

            return _mapper.Map<List<ClassInformationDto>>(classInformations);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

