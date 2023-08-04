using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Review;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries.GetClassInformation;

public class
    GetClassInformationQueryHandler : GetByIdQueryHandler<GetObjectQuery<ClassInformationForDetailDto>, ClassInformationForDetailDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    public GetClassInformationQueryHandler(IClassInformationRepository classInformationRepository,
        IMapper mapper, IRepository<TutorReview> reviewRepository) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;

    }

    public override async Task<Result<ClassInformationForDetailDto>> Handle(GetObjectQuery<ClassInformationForDetailDto> query,
        CancellationToken cancellationToken)
    {
        var classInformation = await _classInformationRepository.GetById(query.ObjectId);
        if (classInformation == null)
        {
            return Result.Fail("The class doesn't exist");
        }
        var classDto = _mapper.Map<ClassInformationForDetailDto>(classInformation);
        return classDto;
    }

}