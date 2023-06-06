using Abp.Application.Services.Dto;

namespace CED.Contracts.Users;

public class TutorVerificationInfoDto : EntityDto<Guid>
{
    public Guid TutorId { get; set; }
    public string Image { get; set; } = "doc_contract.png";
}