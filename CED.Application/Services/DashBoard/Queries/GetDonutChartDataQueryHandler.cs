using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts.Charts;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.DashBoard.Queries;

public class GetDonutChartDataQueryHandler : GetByIdQueryHandler<GetDonutChartDataQuery, DonutChartData>
{
    public GetDonutChartDataQueryHandler(IMapper mapper) : base(mapper)
    {
    }

    public override async Task<DonutChartData?> Handle(GetDonutChartDataQuery query, CancellationToken cancellationToken)
    {
        var startDay = DateTime.Today;
        switch (query.ByTime)
        {
            case "month":
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(29));

                break;
            case "year":
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(355));
                break;
        }


        var classInforsPie = query.ClassInformationDtos
            .Where(x => x.CreationTime >= startDay)
            .GroupBy(x => x.Status)
            .Select((x) => new { key = x.Key, count = x.Count() });


        var pieWeekData = new List<DonutData>();
        foreach (var c in classInforsPie)
        {
            pieWeekData.Add(new DonutData(
                c.count,
                c.key.ToString()
            ));
        }

        return new DonutChartData(pieWeekData);
    }
}