using System.ComponentModel.DataAnnotations;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.Authentication;

public class RegisterRequest
{
    [Required] 
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; }= string.Empty;
    [Required]
    public string Email { get; set; }= string.Empty;
    [Required]
    public string Password { get; set; }= string.Empty;
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public int BirthYear { get; set; } = 1960;
    [Required]
    public string Gender { get; set; } = CED.Domain.Shared.ClassInformationConsts.Gender.Male.ToString();

    
}
    


