using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts.Charts;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.DashBoard.Queries;

public class GetLineChartDataQueryHandler : GetByIdQueryHandler<GetLineChartDataQuery, LineChartData>
{
    public GetLineChartDataQueryHandler(IMapper mapper) : base(mapper)
    {
    }

    public override async Task<LineChartData?> Handle(GetLineChartDataQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        List<int> dates = new List<int>();

        var startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));
        switch (query.ByTime)
        {
            case "month":
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(29));
                for (int i = 0; i < 30; i++)
                {
                    dates.Add(startDay.Day);
                    startDay = startDay.AddDays(1);
                }
                break;
            // case "week":
            //     startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));
            //     for (int i = 0; i < 7; i++)
            //     {
            //         dates.Add(startDay.Day);
            //         startDay = startDay.AddDays(1);
            //     }
            //     break;
            default:
                for (int i = 0; i < 7; i++)
                {
                    dates.Add(startDay.Day);
                    startDay = startDay.AddDays(1);
                }
                break;
        }
        
        startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));

        var classesInWeek = dates.GroupJoin(
                query.ClassInformationDtos
                    .Where(x => x.CreationTime >= startDay)
                    .GroupBy(x => x.CreationTime.Day),
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.FirstOrDefault()?.Count() ?? 0
                })
            .Select(x => x.classInfo)
            .ToList();
        var studentsInWeek = dates.GroupJoin(
                query.StudentDtos
                    .Where(x => x.CreationTime >= startDay)
                    .GroupBy(x => x.CreationTime.Day),
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.FirstOrDefault()?.Count() ?? 0
                })
            .Select(x => x.classInfo)
            .ToList();

        var tutorsInWeek = dates.GroupJoin(
                query.TutorDtos
                    .Where(x => x.CreationTime >= startDay)
                    .GroupBy(x => x.CreationTime.Day),
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.FirstOrDefault()?.Count() ?? 0
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

