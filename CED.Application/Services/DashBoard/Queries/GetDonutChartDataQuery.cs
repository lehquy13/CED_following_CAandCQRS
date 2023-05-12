using CED.Contracts.Charts;
using CED.Contracts.ClassInformations;
using MediatR;

namespace CED.Application.Services.DashBoard.Queries;

public record GetDonutChartDataQuery
(
    List<ClassInformationDto> ClassInformationDtos, 
    string ByTime = ""
): IRequest<DonutChartData>;