using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CED.Application.Services.ClassInformations.Queries;

public class GetLearningClassesOfUserQueryHandler : GetAllQueryHandler<GetLearningClassesOfUserQuery, ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISubjectRepository _subjectRepository;

    public GetLearningClassesOfUserQueryHandler(
        IClassInformationRepository classInformationRepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<PaginatedList<ClassInformationDto>> Handle(GetLearningClassesOfUserQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var student = await _userRepository.GetById(query.Guid);
            if (student is null)
            {
                throw new Exception("The user does not exist!");
            }
            var classInformations = _classInformationRepository
                .GetLearningClassInformationsByUserId(query.Guid)
                .OrderByDescending(x => x.CreationTime)
                .Where(x => x.IsDeleted == false);;
            var subjects =  await _subjectRepository.GetAllList();
            var tutors = _userRepository.GetTutors();
            
            var classInformationDtos =
                _mapper.Map<List<ClassInformationDto>>(classInformations.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize));
            
            var resultPaginatedList = PaginatedList<ClassInformationDto>.CreateAsync(classInformationDtos,
                query.PageIndex, query.PageSize, classInformationDtos.Count);


            foreach (var classIn in resultPaginatedList)
            {
                
                if (subjects.FirstOrDefault(x => x.Id == classIn.SubjectId) is { } subject)
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

