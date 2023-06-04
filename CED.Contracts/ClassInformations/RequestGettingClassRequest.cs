namespace CED.Contracts.ClassInformations;

public class RequestGettingClassRequest
{
    public string Email { get; set; } = string.Empty; 
    public Guid ClassId { get; set; }
};