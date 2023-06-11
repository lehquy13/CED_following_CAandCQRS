using CED.Contracts.ClassInformations.Dtos;

namespace CED.Contracts.Users;

public class TutorProfileDto
{
    public PaginatedList<RequestGettingClassDto> RequestGettingClassDtos = new();
    public TutorMainInfoDto TutorMainInfoDto = new();
}