using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Charts;

namespace CED.Application.Services.DashBoard.Queries;

public class GetDonutChartDataQuery : GetObjectQuery<DonutChartData>
{
    public string ByTime { get; set; } = "";
}