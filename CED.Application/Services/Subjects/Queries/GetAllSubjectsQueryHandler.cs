using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Queries;

public class GetAllSubjectsQueryHandler : GetAllQueryHandler<GetObjectQuery<PaginatedList<SubjectDto>>, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;

    public GetAllSubjectsQueryHandler(ISubjectRepository subjectRepository,IRepository<TutorMajor> tutorMajorRepository, IMapper mapper):base(mapper)
    {
        _subjectRepository = subjectRepository;
        _tutorMajorRepository = tutorMajorRepository;
    }
    public override async Task<Result<PaginatedList<SubjectDto>>> Handle(GetObjectQuery<PaginatedList<SubjectDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            List<Subject> subjects = await _subjectRepository.GetAllList();
            //if ObjectId is not empty, get all the subjects which is tutor's major
            if (query.ObjectId != Guid.Empty)
            {
                var tutorMajors = await _subjectRepository.GetTutorMajors(query.ObjectId);
                //remove tutor's major from all subjects
                subjects = subjects.Except(tutorMajors).ToList();
            }

            var totalSubjects = subjects.Count;
           
            return PaginatedList<SubjectDto>.CreateAsync(_mapper.Map<List<SubjectDto>>(subjects),query.PageIndex,query.PageSize,totalSubjects);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

