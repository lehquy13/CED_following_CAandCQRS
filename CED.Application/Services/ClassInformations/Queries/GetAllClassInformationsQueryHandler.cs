using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQueryHandler : GetAllQueryHandler<GetAllClassInformationsQuery,ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    public GetAllClassInformationsQueryHandler(IClassInformationRepository classInformationRepository, IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<List<ClassInformationDto>> Handle(GetAllClassInformationsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var classInformations = await _classInformationRepository.GetAllList();

            return _mapper.Map<List<ClassInformationDto>>(classInformations);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

