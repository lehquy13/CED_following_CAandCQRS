using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts.Charts;
using CED.Contracts.Subjects;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.DashBoard.Queries;

public class GetLineChartDataQueryHandler : GetByIdQueryHandler<GetLineChartDataQuery, LineChartData>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserRepository _tutorRepository;
    public GetLineChartDataQueryHandler(IMapper mapper, IClassInformationRepository classInformationRepository, IUserRepository userRepository,IUserRepository tutorRepository) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _userRepository = userRepository;
        _tutorRepository = tutorRepository;
    }

    public override async Task<Result<LineChartData>> Handle(GetLineChartDataQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        List<int> dates = new List<int>();

        var startDay = DateTime.Today;
        switch (query.ByTime)
        {
            case "month":
                startDay = startDay.Subtract(TimeSpan.FromDays(29));
                
                for (int i = 0; i < 30; i++)
                {
                    dates.Add(startDay.Day);
                    startDay = startDay.AddDays(1);
                }
                startDay = startDay.Subtract(TimeSpan.FromDays(29));

                break;
            default:
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));

                for (int i = 0; i < 7; i++)
                {
                    dates.Add(startDay.Day);
                    startDay = startDay.AddDays(1);
                }
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));

                break;
        }

        var allClasses = _classInformationRepository.GetAll().Where(x => x.CreationTime >= startDay)
            .GroupBy(x => x.CreationTime.Day).ToList();
        var allLearner =  _userRepository.GetAll()
            .Where(x => x.CreationTime >= startDay)
            .GroupBy(x => x.CreationTime.Day).ToList();
        var allTutor = _tutorRepository.GetAll()
            .Where(x => x.CreationTime >= startDay)
            .GroupBy(x => x.CreationTime.Day).ToList();

        var classesInWeek = dates.Join( 
                allClasses,
                d => d,
                c => c.Key, 
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.Count() 
                })
            .Select(x => x.classInfo)
            .ToList();
        var studentsInWeek = dates.Join(
                allLearner,
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.Count()
                })
            .Select(x => x.classInfo)
            .ToList();

        var tutorsInWeek = dates.Join(
               allTutor,
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.Count()
                })
            .Select(x => x.classInfo)
            .ToList();

        var chartWeekData =  new List<LineData>()
        {
            new LineData(
                "Classes",
                classesInWeek
            ),
            new LineData(
                "Tutors",
                tutorsInWeek
            ),
            new LineData(
                "Students",
                studentsInWeek
            )
        };
      
        
        
        return new LineChartData(chartWeekData, dates) ;
    }
}

