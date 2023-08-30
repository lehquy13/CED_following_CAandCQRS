using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries.GetAllClassInformationsQuery;

public class GetAllClassInformationsQueryHandler : GetAllQueryHandler<GetAllClassInformationsQuery, ClassInformationForListDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;

    public GetAllClassInformationsQueryHandler(
        IClassInformationRepository classInformationRepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper
    ) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<Result<PaginatedList<ClassInformationForListDto>>> Handle(GetAllClassInformationsQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            //Create a list of class query
            var classesQuery = _classInformationRepository.GetAll()
                .OrderByDescending(x => x.CreationTime)
                .Where(x => x.IsDeleted == false);
            //Filter by Today | Verifying | Purchasing | All
            switch (query.Filter)
            {
                case "Today":
                    classesQuery = classesQuery.Where(x => x.CreationTime >= DateTime.Today);
                    break;

                case "Verifying":
                    classesQuery = classesQuery.Where(x => x.Status == Status.OnVerifying);
                    break;
                case "Purchasing":
                    classesQuery = classesQuery.Where(x => x.Status == Status.OnPurchasing);
                    break;
            }


            //Filter by SubjectName if it is not null
            if (!string.IsNullOrWhiteSpace(query.SubjectName))
            {
                var subject = await _subjectRepository.GetSubjectByName(query.SubjectName);

                if (subject is not null)
                {
                    classesQuery = classesQuery.Where(x => x.SubjectId == subject.Id);
                }
            }

            //Filter by Status if it is not null
            if (query.Status is not null)
            {
                classesQuery = classesQuery.Where(x => x.Status == query.Status);
            }

            var classesQueryResult = classesQuery.ToList();
            
            //totalPages after filtering
            var totalPages = classesQuery.Count();

            //Get the class of the page
            var classInformationDtos =
                _mapper.Map<List<ClassInformationForListDto>>(classesQuery.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize).ToList());
            
            //transform the class of the page to PaginatedList
            var resultPaginatedList = PaginatedList<ClassInformationForListDto>.CreateAsync(classInformationDtos,
                query.PageIndex, query.PageSize, totalPages);
            
            //Map subject name attrs and tutor information attrs to the class of the page
            // foreach (var classIn in resultPaginatedList)
            // {
            //     if (await _subjectRepository.GetById(classIn.SubjectId) is { } subject)
            //     {
            //         classIn.SubjectName = subject.Name;
            //     }
            // }

            return resultPaginatedList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}