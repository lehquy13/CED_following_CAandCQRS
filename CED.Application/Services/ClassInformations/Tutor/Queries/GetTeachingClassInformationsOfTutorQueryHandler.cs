using System.Linq.Dynamic.Core;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Tutor.Queries;

public class
    GetTeachingClassInformationsOfTutorQueryHandler : GetAllQueryHandler<GetTeachingClassInformationsOfTutorQuery,
        RequestGettingClassDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepositoryepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;

    public GetTeachingClassInformationsOfTutorQueryHandler(
        IClassInformationRepository classInformationRepository,
        IRepository<RequestGettingClass> requestGettingClassRepositoryepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _requestGettingClassRepositoryepository = requestGettingClassRepositoryepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<PaginatedList<RequestGettingClassDto>> Handle(
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
            var requests = _requestGettingClassRepositoryepository.GetAll()
                .Where(x => x.TutorId.Equals(tutor.Id))
                .ToList();
            var classes = await _classInformationRepository.GetAllList();
            var classInformations = 
                requests.GroupJoin(
                    classes,
                    d=>d.ClassInformationId,
                    c => c.Id,
                    (d,c)=> (d,c.FirstOrDefault()).Adapt<RequestGettingClassDto>()
                );
            //var subjects = await _subjectRepository.GetAllList();
            
            var classInformationsL = classInformations.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize).ToList();

            var resultPaginatedList = PaginatedList<RequestGettingClassDto>.CreateAsync(classInformationsL,
                query.PageIndex, query.PageSize, classInformationsL.Count);


            // foreach (var classIn in resultPaginatedList)
            // {
            //     classIn.TutorPhoneNumber = tutor.PhoneNumber;
            //     classIn.TutorEmail = tutor.Email;
            //     classIn.TutorName = tutor.FirstName + " " + tutor.LastName;
            //     if (subjects.FirstOrDefault(x => x.Id == classIn.SubjectId) is Subject subject)
            //     {
            //         classIn.SubjectName = subject.Name;
            //     }
            //  
            // }

            return resultPaginatedList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}