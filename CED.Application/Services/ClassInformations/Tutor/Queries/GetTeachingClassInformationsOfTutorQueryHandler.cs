using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Tutor.Queries;

public class
    GetTeachingClassInformationsOfTutorQueryHandler : GetAllQueryHandler<GetTeachingClassInformationsOfTutorQuery,
        ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;

    public GetTeachingClassInformationsOfTutorQueryHandler(
        IClassInformationRepository classInformationRepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<PaginatedList<ClassInformationDto>> Handle(
        GetTeachingClassInformationsOfTutorQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var tutor = await _userRepository.GetById(query.Guid);
            if (tutor is null)
            {
                throw new Exception("The user does not exist!");
            }
            var classInformations = _classInformationRepository.GetTeachingClassInformationsByUserId(query.Guid);
            var subjects = await _subjectRepository.GetAllList();


            var classInformationDtos =
                _mapper.Map<List<ClassInformationDto>>(classInformations.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize));

            var resultPaginatedList = PaginatedList<ClassInformationDto>.CreateAsync(classInformationDtos,
                query.PageIndex, query.PageSize, classInformationDtos.Count);


            foreach (var classIn in resultPaginatedList)
            {
                classIn.TutorPhoneNumber = tutor.PhoneNumber;
                classIn.TutorEmail = tutor.Email;
                classIn.TutorName = tutor.FirstName + " " + tutor.LastName;
                if (subjects.FirstOrDefault(x => x.Id == classIn.SubjectId) is Subject subject)
                {
                    classIn.SubjectName = subject.Name;
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