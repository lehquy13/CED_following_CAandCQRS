using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Charts;
using CED.Domain.ClassInformations;
using CED.Domain.Shared.ClassInformationConsts;
using FluentResults;
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

    public override async Task<Result<AreaChartData>> Handle(GetAreaChartDataQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Create a dateListRange by the query.ByTime
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

        var allClasses = await _classInformationRepository.GetAllList();
        var confirmedClasses = dates.Join(
            allClasses
                .Where(x => x.CreationTime >= startDay && x.Status == Status.Confirmed)
                .GroupBy(x => x.CreationTime.Day), // Group by day of time range, then merge with dates
            d => d,
            c => c.Key,
            (d, c) => new
            {
                dates = d,
                sum = c.Sum(r => r.ChargeFee)
            });
        var canceledClasses = dates.Join(
            allClasses
                .Where(x => x.CreationTime >= startDay && x.Status == Status.Canceled)
                .GroupBy(x => x.CreationTime.Day),
            d => d,
            c => c.Key,
            (d, c) => new
            {
                dates = d,
                sum = c.Sum(r => r.ChargeFee)
            });
        var onPurchasingClasses = dates.Join(
            allClasses
                .Where(x => x.CreationTime >= startDay && x.Status == Status.OnPurchasing)
                .GroupBy(x => x.CreationTime.Day),
            d => d,
            c => c.Key,
            (d, c) => new
            {
                dates = d,
                sum = c.Sum(r => r.ChargeFee)
            });


        List<float> resultInts = confirmedClasses
            .Select(x => x.sum)
            .ToList();
        if (resultInts.Count <= 0)
        {
            resultInts.Add(1);
        }

        List<float> resultInts1 = canceledClasses
            .Select(x => x.sum)
            .ToList();
        if (resultInts1.Count <= 0)
        {
            resultInts1.Add(1);
        }

        List<float> resultInts2 = onPurchasingClasses
            .Select(x => x.sum)
            .ToList();
        if (resultInts2.Count <= 0)
        {
            resultInts2.Add(1);
        }

        var resultStrings = dates
            .Select(x => x.ToString()).ToList();
        
        if (resultStrings.Count <= 0)
        {
            resultStrings.Add("None");
        }


        return new AreaChartData
        (
            new AreaData("Total Revenues", resultInts),
            new AreaData("Charged", resultInts2),
            new AreaData("Refunded", resultInts1),
            resultStrings
        );
    }
}