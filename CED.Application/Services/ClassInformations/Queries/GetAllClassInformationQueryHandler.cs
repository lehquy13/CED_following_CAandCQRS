using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationQueryHandler
    : IRequestHandler<GetAllClassInformationQuery, List<ClassInformationDto>>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IMapper _mapper;
    public GetAllClassInformationQueryHandler(IClassInformationRepository classInformationRepository, IMapper mapper)
    {
        _classInformationRepository = classInformationRepository;
        _mapper = mapper;
    }
    public async Task<List<ClassInformationDto>> Handle(GetAllClassInformationQuery query, CancellationToken cancellationToken)
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

