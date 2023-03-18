using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetClassInformationQueryHandler
    : IRequestHandler<GetClassInformationQuery, ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IMapper _mapper;
    public GetClassInformationQueryHandler(IClassInformationRepository classInformationRepository, IMapper mapper)
    {
        _classInformationRepository = classInformationRepository;
        _mapper = mapper;
    }
    public async Task<ClassInformationDto> Handle(GetClassInformationQuery query, CancellationToken cancellationToken)
    {

        var classInformation = await _classInformationRepository.GetById(query.id);
        if (classInformation == null)
        {
            throw new Exception("the class doesn't exist.");

        }
        return _mapper.Map<ClassInformationDto>(classInformation);


    }
}

