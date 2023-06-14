using Abp.Application.Services.Dto;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.Authentication;

public class UserLoginDto
{
    private Guid Id { get; set; }

    //User information
    public string FullName { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    //Account References
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Learner;
}

