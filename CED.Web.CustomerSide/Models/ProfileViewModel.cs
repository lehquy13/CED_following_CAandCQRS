using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Web.CustomerSide.Models;

public class ProfileViewModel
{
    public CED.Contracts.Users.LearnerDto UserDto { get; set; } = new();
    public CED.Contracts.Users.TutorMainInfoDto TutorDto { get; set; } = new();
    public PaginatedList<RequestGettingClassDto> RequestGettingClassDtos { get; set; } = new();

    public CED.Contracts.Authentication.ChangePasswordRequest ChangePasswordRequest { get; set; } = new();
    public bool IsPartialLoad { get; set; } = false;

}