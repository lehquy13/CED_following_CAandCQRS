using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Tutor.Queries.GetAllRequestGettingClassOfTutor;

public class
    GetAllRequestGettingClassOfTutorQueryHandler : GetAllQueryHandler<GetAllRequestGettingClassOfTutorQuery,
        RequestGettingClassForListDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IUserRepository _userRepository;

    public GetAllRequestGettingClassOfTutorQueryHandler(
        IClassInformationRepository classInformationRepository,
        IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _userRepository = userRepository;
    }

    public override async Task<Result<PaginatedList<RequestGettingClassForListDto>>> Handle(
        GetAllRequestGettingClassOfTutorQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            //Check if user exists or not
            var tutor = await _userRepository.GetById(query.ObjectId);
            if (tutor is null)
            {
                return Result.Fail(new NonExistUserError());
            }
            // Get all the requests of the user
            var teachingClassRequestsFromDb = await _classInformationRepository.GetAllClassRequestsByUserId(query.ObjectId);
            var teachingClassRequestDtos = _mapper.Map<List<RequestGettingClassForListDto>>(teachingClassRequestsFromDb);
          
            var resultPaginatedList = PaginatedList<RequestGettingClassForListDto>.CreateAsync(teachingClassRequestDtos,
                query.PageIndex, query.PageSize, teachingClassRequestDtos.Count);

            return resultPaginatedList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}