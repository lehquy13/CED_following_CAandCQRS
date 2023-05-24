using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetClassInformationQueryHandler : GetByIdQueryHandler<GetObjectQuery<ClassInformationDto>, ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;

    public GetClassInformationQueryHandler(IClassInformationRepository classInformationRepository,
                                           ISubjectRepository subjectRepository,
                                           IUserRepository userRepository,
                                           IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<ClassInformationDto?> Handle(GetObjectQuery<ClassInformationDto> query, CancellationToken cancellationToken)
    {
        var classInformation = await _classInformationRepository.GetById(query.Guid);

        if (classInformation == null)
        {
            return null;
        }
        var subject = await _subjectRepository.GetById(classInformation.SubjectId);
        if(classInformation.TutorId is not null)
        {
            var tutor = await _userRepository.GetById((Guid)classInformation.TutorId);
            return (classInformation, subject, tutor).Adapt<ClassInformationDto>();

        }

        //return _mapper.Map<(ClassInformation, Subject), ClassInformationDto>(new {  });
        return (classInformation, subject).Adapt<ClassInformationDto>();
    }
}

