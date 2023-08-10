using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Charts;

namespace CED.Application.Services.DashBoard.Queries;

public class GetLineChartDataQuery : GetObjectQuery<LineChartData>
{
    public string ByTime { get; set; } = "";
}