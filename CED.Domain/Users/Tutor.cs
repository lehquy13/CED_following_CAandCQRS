using Abp.Domain.Entities;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;

public class Tutor : Entity<Guid>
{
    //is tutor related informtions
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Student;
    public string? University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;

    
    /// <summary>
    /// Update tutor's information and change the state into being verified
    /// </summary>
    /// <param name="tutor"></param>
    public void UpdateTutorInformation(Tutor tutor)
    {
        //ase.UpdateUserInformation(tutor);
        AcademicLevel = tutor.AcademicLevel;
        University = tutor.University;

        //wait for being verified
        IsVerified = false;
    }
    public void VerifyTutorInformation(Tutor tutor)
    {
        UpdateTutorInformation(tutor);
        IsVerified = true;
    }

   
}

public class TutorFull : User
{
    //is tutor related informtions
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Student;
    public string? University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;

    public TutorFull(User user, Tutor tutor)
    {
        Id = user.Id;
        AcademicLevel = tutor.AcademicLevel;
        Rate = tutor.Rate;
        IsVerified = tutor.IsVerified;
        University = tutor.University;

        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        IsEmailConfirmed = user.IsEmailConfirmed;
        PhoneNumber = user.PhoneNumber;
        
        Gender = user.Gender;
        BirthYear = user.BirthYear;
        Address = user.Address;
        Description = user.Description;
        Image = user.Image;
        Role = user.Role;
        
        
    }
    
    /// <summary>
    /// Update tutor's information and change the state into being verified
    /// </summary>
    /// <param name="tutor"></param>
    public void UpdateTutorInformation(Tutor tutor)
    {
        //ase.UpdateUserInformation(tutor);
        AcademicLevel = tutor.AcademicLevel;
        University = tutor.University;

        //wait for being verified
        IsVerified = false;
    }
    public void VerifyTutorInformation(Tutor tutor)
    {
        UpdateTutorInformation(tutor);
        IsVerified = true;
    }

   
}