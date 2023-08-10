using CED.Contracts.ClassInformations.Dtos;

namespace CED.Contracts.Users.Tutors;

public class TutorProfileDto
{
    public PaginatedList<RequestGettingClassForListDto> RequestGettingClassForListDtos = new();
    public TutorMainInfoDto TutorMainInfoDto = new();
}