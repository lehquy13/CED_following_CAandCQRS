using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries.GetAllRequestGettingClass;

public class GetAllRequestOfClassQueryHandler : GetAllListQueryHandler<GetAllRequestOfClassQuery, RequestGettingClassDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    public GetAllRequestOfClassQueryHandler(
        IClassInformationRepository classInformationRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<Result<List<RequestGettingClassDto>>> Handle(
        GetAllRequestOfClassQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            
            var classInformation = await _classInformationRepository.GetAllClassWithRequest(query.ClassId);
            if (classInformation is null)
            {
                return Result.Fail("The class doesn't exist");
            }
            
            var requestGettingClassDtos = _mapper.Map<List<RequestGettingClassDto>>(classInformation.RequestGettingClasses);
            return requestGettingClassDtos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}