using CED.Contracts.Charts;
using MediatR;

namespace CED.Application.Services.DashBoard.Queries;

public record GetDonutChartDataQuery
(
    string ByTime = ""
): IRequest<DonutChartData>;