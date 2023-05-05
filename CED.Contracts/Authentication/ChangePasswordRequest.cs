using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CED.Contracts.Authentication;
public class ChangePasswordRequest
{
    [Required]
    public Guid Id { get; set; } 

    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [Compare("NewPassword")]
    public string ConfirmedPassword { get; set; } = string.Empty;
}

