using Castle.Core.Internal;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Charts;
using CED.Domain.ClassInformations;
using CED.Domain.Shared.ClassInformationConsts;
using MapsterMapper;

namespace CED.Application.Services.DashBoard.Queries;

public class GetAreaChartDataQueryHandler : GetByIdQueryHandler<GetAreaChartDataQuery, AreaChartData>
{
    private readonly IClassInformationRepository _classInformationRepository;

    public GetAreaChartDataQueryHandler(IMapper mapper, IClassInformationRepository classInformationRepository) :
        base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<AreaChartData?> Handle(GetAreaChartDataQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

     
        List<int> dates = new List<int>();

        var startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));
        switch (query.ByTime)
        {
            case ByTime.Month:
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(29));
                for (int i = 0; i < 30; i++)
                {
                    dates.Add(startDay.Day);
                    startDay = startDay.AddDays(1);
                }
                break;
            case ByTime.Week:
                for (int i = 0; i < 7; i++)
                {
                    dates.Add(startDay.Day);
                    startDay = startDay.AddDays(1);
                }
                break;
        }
        startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));

       
        var classesInWeek = dates.GroupJoin(
            _classInformationRepository.GetAll()
                .Where(x => x.CreationTime >= startDay && x.Status == Status.Confirmed)
                .GroupBy(x => x.CreationTime.Day),
            d => d,
            c => c.Key,
            (d, c) => new
            {
                dates = d,
                sum = c.FirstOrDefault()?.Sum(r => r.ChargeFee)
            });
        var classesInWeek1 = dates.GroupJoin(
            _classInformationRepository.GetAll()
                .Where(x => x.CreationTime >= startDay && x.Status == Status.Canceled)
                .GroupBy(x => x.CreationTime.Day),
            d => d,
            c => c.Key,
            (d, c) => new
            {
                dates = d,
                sum = c.FirstOrDefault()?.Sum(r => r.ChargeFee)
            });
        var classesInWeek2 = dates.GroupJoin(
            _classInformationRepository.GetAll()
                .Where(x => x.CreationTime >= startDay && x.Status == Status.OnConfirming)
                .GroupBy(x => x.CreationTime.Day),
            d => d,
            c => c.Key,
            (d, c) => new
            {
                dates = d,
                sum = c.FirstOrDefault()?.Sum(r => r.ChargeFee)
            });
           

        List<float> resultInts = classesInWeek
            .Select(x => x.sum ?? 0)
            .ToList();
        if (resultInts.IsNullOrEmpty())
        {
            resultInts.Add(1);
        }
        List<float> resultInts1 = classesInWeek1
            .Select(x => x.sum ?? 0)
            .ToList();
        if (resultInts1.IsNullOrEmpty())
        {
            resultInts.Add(1);
        }
        List<float> resultInts2 = classesInWeek2
            .Select(x => x.sum ?? 0)
            .ToList();
        if (resultInts2.IsNullOrEmpty())
        {
            resultInts.Add(1);
        }

        List<string> resultStrings = classesInWeek
            .Select(x => x.dates.ToString())
            .ToList();
        if (resultStrings.IsNullOrEmpty())
        {
            resultStrings.Add("None");
        }


        return new AreaChartData
        (
            new AreaData("Total Revenues",resultInts ),
            new AreaData("Charged",resultInts2),
            new AreaData( "Refunded",resultInts1),
            resultStrings
        );
    }
}