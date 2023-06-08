using Abp.Application.Services.Dto;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations.Dtos;

public class RequestGettingClassDto : EntityDto<Guid>
{
    public Guid TutorId { get; set; }
    public Guid ClassInformationId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}