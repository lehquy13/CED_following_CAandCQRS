using CED.Contracts.Charts;
using CED.Contracts.ClassInformations;
using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.DashBoard.Queries;

public record GetLineChartDataQuery 
(
    List<ClassInformationDto> ClassInformationDtos,
    List<StudentDto> StudentDtos,
    List<TutorDto> TutorDtos,
    string ByTime
): IRequest<LineChartData>;