using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;

public class Learner : User
{
    /// <summary>
    /// Update current user to be tutor
    /// </summary>
    /// <param name="tutor"></param>

    public Learner()
    {
        Role = UserRole.Learner;
    }
    public Tutor RegisterToBeTutor(Tutor tutor)
    {
        tutor.Id = Id;
        return tutor;
    }
}