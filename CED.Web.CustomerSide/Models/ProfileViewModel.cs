using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Web.CustomerSide.Models;

public class ProfileViewModel
{
    public CED.Contracts.Users.UserDto UserDto { get; set; } = new();
    public PaginatedList<RequestGettingClassDto> ClassInformationDtos { get; set; } = 
        PaginatedList<RequestGettingClassDto>.CreateAsync(new List<RequestGettingClassDto>(),0,0);
    public CED.Contracts.Authentication.ChangePasswordRequest ChangePasswordRequest { get; set; } = new();
    public bool IsPartialLoad { get; set; } = false;

}