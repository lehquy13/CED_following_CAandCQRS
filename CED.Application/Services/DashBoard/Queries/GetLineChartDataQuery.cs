using CED.Contracts.Charts;
using CED.Contracts.ClassInformations;
using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.DashBoard.Queries;

public record GetLineChartDataQuery 
(
    string ByTime
): IRequest<LineChartData>;