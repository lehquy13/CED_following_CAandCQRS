using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQueryHandler : GetAllQueryHandler<GetObjectQuery<List<ClassInformationDto>>,ClassInformationDto>
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

    public override async Task<List<ClassInformationDto>> Handle(GetObjectQuery<List<ClassInformationDto>> query, CancellationToken cancellationToken)
    {
        try
        {
            var classInformations = await _classInformationRepository.GetAllList();
            var subjects = await _subjectRepository.GetAllList();
            var tutors = _userRepository.GetTutors();

            var classInformationDtos = _mapper.Map<List<ClassInformationDto>>(classInformations.Where(x => x.IsDeleted == false).ToList());

            

            foreach (var classIn in classInformationDtos)
            {
                if( subjects.FirstOrDefault( x => x.Id == classIn.SubjectId ) is Subject subject)
                {
                    classIn.SubjectName = subject.Name;
                }
                if( tutors.FirstOrDefault( x => x.Id == classIn.TutorDtoId ) is User user)
                {
                    classIn.TutorDtoId = user.Id;
                    classIn.TutorPhoneNumber = user.PhoneNumber;
                    classIn.TutorEmail = user.Email;
                    classIn.TutorName = user.FirstName + " " + user.LastName;
                }

            }

            return classInformationDtos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

