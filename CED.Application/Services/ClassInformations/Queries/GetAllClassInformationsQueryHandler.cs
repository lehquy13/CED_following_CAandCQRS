using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQueryHandler
    : IRequestHandler<GetAllClassInformationsQuery, List<ClassInformationDto>>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IMapper _mapper;
    public GetAllClassInformationsQueryHandler(IClassInformationRepository classInformationRepository, IMapper mapper)
    {
        _classInformationRepository = classInformationRepository;
        _mapper = mapper;
    }
    public async Task<List<ClassInformationDto>> Handle(GetAllClassInformationsQuery query, CancellationToken cancellationToken)
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

