using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Models;
using CED.Contracts.Subjects;
using CED.Contracts.TutorReview;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.Users.Tutors;
public class TutorMainInfoDto : FullAuditedAggregateRootDto<Guid>
{
    //is tutor related informtions
    public UserRole Role { get; set; } = UserRole.Tutor;
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Student;
    public string University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;
    public List<SubjectDto> Majors { get; set; } = new();
    public List<TutorVerificationInfoDto> TutorVerificationInfoDtos { get; set; } = new();

}