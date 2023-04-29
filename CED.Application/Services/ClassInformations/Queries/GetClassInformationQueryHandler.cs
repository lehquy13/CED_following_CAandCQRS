using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetClassInformationQueryHandler : GetByIdQueryHandler<GetClassInformationQuery, ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly ISubjectRepository _subjectRepository;

    public GetClassInformationQueryHandler(IClassInformationRepository classInformationRepository, ISubjectRepository subjectRepository, IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
    }

    public override async Task<ClassInformationDto?> Handle(GetClassInformationQuery query, CancellationToken cancellationToken)
    {
        var classInformation = await _classInformationRepository.GetById(query.Id);

        if (classInformation == null)
        {
            return null;
        }
        var subject = await _subjectRepository.GetById(classInformation.SubjectId);

        //return _mapper.Map<(ClassInformation, Subject), ClassInformationDto>(new {  });
        return (classInformation, subject).Adapt<ClassInformationDto>();
    }
}

