using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;

namespace CED.Web.CustomerSide.Models;

public class ClassInformationDetailViewModel
{
    public ClassInformationDto ClassInformationDto { get; set; } = new();
    public PaginatedList<ClassInformationDto> RelatedClasses { get; set; } = PaginatedList<ClassInformationDto>.CreateAsync(new List<ClassInformationDto>(){},0,0);
    
}