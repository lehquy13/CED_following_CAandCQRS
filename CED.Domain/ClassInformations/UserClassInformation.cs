namespace CED.Domain.ClassInformations;

public class UserClassInformation { 

    //Subject related information
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid ClassInformationId { get; set; }
    public bool isTutor { get; set; } = false;

}
