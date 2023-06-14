using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQueryHandler : GetAllQueryHandler<GetAllClassInformationsQuery, ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;

    public GetAllClassInformationsQueryHandler(
        IClassInformationRepository classInformationRepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<PaginatedList<ClassInformationDto>> Handle(GetAllClassInformationsQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var classInformations = _classInformationRepository.GetAll()
                .OrderByDescending(x => x.CreationTime)
                .Where(x => x.IsDeleted == false);
            var subjects = await _subjectRepository.GetAllList();
            var tutors = _userRepository.GetTutors();

            if (!string.IsNullOrWhiteSpace(query.SubjectName))
            {
                var subjs = subjects.Where(x => query.SubjectName.ToLower().Contains(x.Name.ToLower()))
                    .Select(x => x.Id);
                classInformations = classInformations.Where(x => subjs.Contains(x.SubjectId));
            }
            if (query.Status is not null)
            {
                classInformations = classInformations.Where(x => x.Status == query.Status);
            }
            var totalPages = classInformations.Count();

            var classInformationDtos =
                _mapper.Map<List<ClassInformationDto>>(classInformations.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize).ToList());
            
            var resultPaginatedList = PaginatedList<ClassInformationDto>.CreateAsync(classInformationDtos,
                query.PageIndex, query.PageSize, totalPages);


            foreach (var classIn in resultPaginatedList)
            {
                if (subjects.FirstOrDefault(x => x.Id == classIn.SubjectId) is Subject subject)
                {
                    classIn.SubjectName = subject.Name;
                }

                if (tutors.FirstOrDefault(x => x.Id == classIn.TutorDtoId) is User user)
                {
                    //classIn.TutorDtoId = user.Id;
                    classIn.TutorPhoneNumber = user.PhoneNumber;
                    classIn.TutorEmail = user.Email;
                    classIn.TutorName = user.FirstName + " " + user.LastName;
                }
            }

            return resultPaginatedList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}