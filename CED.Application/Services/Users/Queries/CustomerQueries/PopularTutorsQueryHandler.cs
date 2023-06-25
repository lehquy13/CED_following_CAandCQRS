using System.Linq.Dynamic.Core;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.Users.Queries.CustomerQueries;

public class PopularTutorsQueryHandler : IRequestHandler<PopularTutorsQuery,List<TutorDto>>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IMapper _mapper;
    private readonly IClassInformationRepository _classInformationRepository;

    public PopularTutorsQueryHandler(ITutorRepository tutorRepository, 
        IMapper mapper, 
        IClassInformationRepository classInformationRepository
        )
    {
        _tutorRepository = tutorRepository;
        _mapper = mapper;
        _classInformationRepository = classInformationRepository;
        
    }

    public async Task<List<TutorDto>> Handle(PopularTutorsQuery request, CancellationToken cancellationToken)
    {
        var thisMonth = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            1
        );

        var classesInMonth = _classInformationRepository
            .GetAll()
            .Where(x => x.CreationTime >= thisMonth && x.Status == Status.Available)
            .GroupBy(x => x.TutorId)
            .Select(x => x.Key)
            .ToList();

        var tutors = (await _tutorRepository.GetAllsWithFullInformation())
            .GroupJoin(
                classesInMonth,
                tutor => tutor.Id,
                classesInMonth => classesInMonth,
                (tutor, classesInMonth) => new
                {
                    user = tutor,
                    count = classesInMonth.Count()
                }
                
            )
            .OrderByDescending(x=>x.count)
            .Select(x => x.user)
            .Take(6).ToList();
        return _mapper.Map<List<TutorDto>>(tutors);

    }
}