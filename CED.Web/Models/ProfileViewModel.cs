namespace CED.Web.Models;

public class ProfileViewModel
{
    public CED.Contracts.Users.UserDto UserDto { get; set; } = new();
    public CED.Contracts.Authentication.ChangePasswordRequest ChangePasswordRequest { get; set; } = new();
    
}