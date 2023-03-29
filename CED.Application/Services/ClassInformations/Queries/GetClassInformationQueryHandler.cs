using CED.Application.Common.Services.QueryHandlers;
using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetClassInformationQueryHandler : GetByIdQueryHandler<GetClassInformationQuery, ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;

    public GetClassInformationQueryHandler(IClassInformationRepository classInformationRepository, IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<ClassInformationDto?> Handle(GetClassInformationQuery query, CancellationToken cancellationToken)
    {
        var classInformation = await _classInformationRepository.GetById(query.id);
        if (classInformation == null)
        {
            throw new Exception("the class doesn't exist.");

        }
        return _mapper.Map<ClassInformationDto>(classInformation);
    }
}

