﻿using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;
/// <summary>
/// Deprecated! Use GetAllTutorInformationsAdvancedQuery instead!
/// </summary>
public class GetAllTutorsQueryHandler : GetAllQueryHandler<GetObjectQuery<List<TutorDto>>, TutorDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly ISubjectRepository _subjectRepository;


    public GetAllTutorsQueryHandler(IUserRepository userRepository,        ISubjectRepository subjectRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;

        _userRepository = userRepository;
        _tutorMajorRepository = tutorMajorRepository;

    }

    public override async Task<List<TutorDto>> Handle(GetObjectQuery<List<TutorDto>> query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects = _subjectRepository.GetAll();
            var tutors = _userRepository.GetTutors();
            var tutorsMajors = _tutorMajorRepository.GetAll()
                .GroupBy(t => t.TutorId)
                .Select(major => new
                {
                    tutorId = major.Key,
                    majorId = major.ToList()
                });
            
            var tutorDtos = _mapper.Map<List<TutorDto>>(tutors);
           
            foreach (var t in tutorDtos)
            {
                var objectMajor = tutorsMajors.FirstOrDefault(x => x.tutorId.Equals(t.Id));
                if (objectMajor != null)
                {
                    foreach (var majorId in objectMajor.majorId)
                    {
                        var sub = subjects.FirstOrDefault(x => x.Id.Equals(majorId));
                        if (sub is not null)
                        {
                            t.Majors.Add(_mapper.Map<SubjectDto>(sub));
                        }
                    }
                }
            }
            
            var result = _mapper.Map<List<TutorDto>>(
                tutors.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
                    .ToList()
            );
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}