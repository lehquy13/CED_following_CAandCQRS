using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Web.CustomerSide.Models;

public class ProfileViewModel
{
    public CED.Contracts.Users.UserDto UserDto { get; set; } = new();
    public PaginatedList<ClassInformationDto> ClassInformationDtos { get; set; } = 
        PaginatedList<ClassInformationDto>.CreateAsync(new List<ClassInformationDto>(),0,0);
    public CED.Contracts.Authentication.ChangePasswordRequest ChangePasswordRequest { get; set; } = new();
    
}