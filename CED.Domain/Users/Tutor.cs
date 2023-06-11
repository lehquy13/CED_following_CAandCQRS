using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;

public class Tutor : User
{
    //is tutor related informtions
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Student;
    public string? University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;

    public Tutor()
    {
        Role = UserRole.Tutor;
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