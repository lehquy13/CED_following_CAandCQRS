using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetAllClassInformationsQueryHandler : GetAllQueryHandler<GetAllClassInformationsQuery,ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly ISubjectRepository _subjectRepository;

    public GetAllClassInformationsQueryHandler(IClassInformationRepository classInformationRepository, ISubjectRepository subjectRepository ,IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
    }

    public override async Task<List<ClassInformationDto>> Handle(GetAllClassInformationsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var classInformations = await _classInformationRepository.GetAllList();
            var subjects = (await _subjectRepository.GetAllList());

            var classInformationDtos = _mapper.Map<List<ClassInformationDto>>(classInformations);



            foreach (var classIn in classInformationDtos)
            {
                if( subjects.FirstOrDefault( x => x.Id == classIn.SubjectId ) is Subject subject)
                {
                    classIn.SubjectName = subject.Name;
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

