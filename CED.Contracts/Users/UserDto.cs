using Abp.Application.Services.Dto;
using CED.Contracts.Subjects;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.Users;
public class UserDto : FullAuditedEntityDto<Guid>
{
    //User information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Male;
    public int BirthYear { get; set; } = 1960;
    //public string WardId { get; set; } = "00001";
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;


    //Account References
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; } = false;

    public string PhoneNumber { get; set; } = string.Empty;
  
    public string? Password { get; set; } = "1q2w3E*";

    //is tutor related informtions
    public UserRole Role { get; set; } = UserRole.Student;
    public AcademicLevel AcademicLevel { get; set; }
    public string? University { get; set;} = string.Empty;
    public bool IsVerified { get; set; } = false;

    public List<SubjectDto> SubjectDtos = new();


}

