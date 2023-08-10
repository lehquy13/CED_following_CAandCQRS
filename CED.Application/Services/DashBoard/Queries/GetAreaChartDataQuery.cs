using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Charts;
using MediatR;

namespace CED.Application.Services.DashBoard.Queries;

/// <summary>
/// Get financial data for Area Chart
/// </summary>
/// <param name="ByTime"></param>
public class GetAreaChartDataQuery : GetObjectQuery<AreaChartData>
{
    public string ByTime = "";
}