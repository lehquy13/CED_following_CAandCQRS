using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Application.Services.Users.Queries.CustomerQueries;

public class GetAllTutorInformationsAdvancedQuery : GetObjectQuery<List<TutorDto>>
{
    public string SubjectName { get; set; } = string.Empty;
    public string TutorName { get; set; } = string.Empty;
    public int BirthYear { get; set; } = 0;
    public AcademicLevel Academic { get; set; } = AcademicLevel.Optional;
    public Gender Gender { get; set; } = Gender.None;
    public string Address { get; set; } = string.Empty;


}