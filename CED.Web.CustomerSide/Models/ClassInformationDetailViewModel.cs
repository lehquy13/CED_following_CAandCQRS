using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Web.CustomerSide.Models;

public class ClassInformationDetailViewModel
{
    public ClassInformationForDetailDto ClassInformationDto { get; set; } = new();
    public PaginatedList<ClassInformationForListDto> RelatedClasses { get; set; } = PaginatedList<ClassInformationForListDto>.CreateAsync(new List<ClassInformationForListDto>(){},0,0);
    
}