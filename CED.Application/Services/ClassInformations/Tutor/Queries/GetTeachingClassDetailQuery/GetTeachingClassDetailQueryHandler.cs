using CED.Application.Common.Errors.ClassInformations;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Tutor.Queries.GetTeachingClassDetailQuery;

public class GetTeachingClassDetailQueryHandler 
    : GetByIdQueryHandler<GetTeachingClassDetailQuery, RequestGettingClassExtendDto>

{
    private readonly IClassInformationRepository _classInformationRepository;

    public GetTeachingClassDetailQueryHandler(
        IClassInformationRepository classInformationRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<Result<RequestGettingClassExtendDto>> Handle(GetTeachingClassDetailQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var classInformation = await _classInformationRepository.GetById(query.ClassInformationId);
            if (classInformation is null)
            {
                return Result.Fail(new NonExistClassError());
            }

            var requestFromDb = classInformation.RequestGettingClasses.FirstOrDefault(x => x.Id == query.ObjectId);
            if (requestFromDb is null)
            {
                return Result.Fail(new NonExistRequestError());
            }
            var resylt =_mapper.Map<RequestGettingClassExtendDto>(requestFromDb);
            return resylt;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    
}