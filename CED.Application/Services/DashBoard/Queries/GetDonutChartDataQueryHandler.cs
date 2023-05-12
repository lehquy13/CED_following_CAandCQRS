using Castle.Core.Internal;
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

    public override async Task<DonutChartData?> Handle(GetDonutChartDataQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var startDay = DateTime.Today;
        switch (query.ByTime)
        {
            case "month":
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(29));

                break;
            case "week":
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));
                break;
        }


        var classInforsPie = query.ClassInformationDtos
            .Where(x => x.CreationTime >= startDay)
            .GroupBy(x => x.Status)
            .Select((x) => new { key = x.Key.ToString(), count = x.Count() });


        List<int> resultInts = classInforsPie
            .Select(x => x.count)
            .ToList();
        if (resultInts.IsNullOrEmpty())
        {
            resultInts.Add(1);
        }
        List<string> resultStrings = classInforsPie
            .Select(x => x.key)
            .ToList();
        if (resultStrings.IsNullOrEmpty())
        {
            resultStrings.Add("None");
        }


        return new DonutChartData(resultInts, resultStrings);
    }
}