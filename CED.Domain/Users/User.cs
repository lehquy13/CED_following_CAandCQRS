using Abp.Domain.Entities.Auditing;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;
public class User : FullAuditedAggregateRoot<Guid>
{
    //User information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Male;
    public int BirthYear { get; set; } = 1960;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    //Account References
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; } = false;

    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = "1q2w3E*";

    //is tutor related informtions
    public UserRole Role { get; set; } = UserRole.Student;
    public AcademicLevel AcademicLevel { get; set; }
    public string University { get; set;} = string.Empty;
    public bool isVerified { get; set; } = false;

    /// <summary>
    /// Update current user to be tutor
    /// </summary>
    /// <param name="tutor"></param>

    public void RegisterToBeTutor(User tutor)
    {
        Role = tutor.Role;
        AcademicLevel = tutor.AcademicLevel;
        University = tutor.University;
    }

    /// <summary>
    /// Update tutor's information and change the state into being verified
    /// </summary>
    /// <param name="tutor"></param>

    public void UpdateTutorInformation(User tutor)
    {
        FirstName = tutor.FirstName;
        LastName = tutor.LastName;
        Gender = tutor.Gender;
        BirthYear = tutor.BirthYear;
        Address = tutor.Address;
        Description = tutor.Description;

        PhoneNumber = tutor.PhoneNumber;

        AcademicLevel = tutor.AcademicLevel;
        University = tutor.University;

        //wait for being verified
        isVerified = false;

    }

    /// <summary>
    /// Update standard user's information
    /// </summary>
    /// <param name="user"></param>
    public void UpdateUserInformation(User user)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Gender = user.Gender;
        BirthYear = user.BirthYear;
        Address = user.Address;
        Description = user.Description;

        PhoneNumber = user.PhoneNumber;
    }
}

