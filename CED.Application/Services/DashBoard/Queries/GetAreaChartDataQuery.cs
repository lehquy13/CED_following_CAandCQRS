using CED.Contracts.Charts;
using MediatR;

namespace CED.Application.Services.DashBoard.Queries;

public record GetAreaChartDataQuery
(
    string ByTime = ""
): IRequest<AreaChartData>;