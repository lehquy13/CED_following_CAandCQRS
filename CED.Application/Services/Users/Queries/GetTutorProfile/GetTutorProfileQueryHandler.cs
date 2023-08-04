using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.GetTutorProfile;

public class
    GetTutorProfileQueryHandler : GetByIdQueryHandler<GetTutorProfileQuery, TutorProfileDto>
{
    private readonly ITutorRepository _tutorRepository;

    public GetTutorProfileQueryHandler(
        ITutorRepository tutorRepository,
        IMapper mapper) : base(mapper)
    {
        _tutorRepository = tutorRepository;
    }

    public override async Task<Result<TutorProfileDto>> Handle(
        GetTutorProfileQuery query, CancellationToken cancellationToken)
    {
        //This task need to pull tutor data: basic information, class information, verification information, major information
        try
        {
            var tutor = await _tutorRepository.GetById(query
                .ObjectId); // This query includes verification information, major information, requests getting class
            if (tutor is null)
            {
                return Result.Fail(new NonExistTutorError());
            }

            return _mapper.Map<TutorProfileDto>(tutor);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}