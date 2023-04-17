using Abp.Application.Services.Dto;

namespace CED.Web.Models;

public class SubjectViewModel : EntityDto<Guid>
{

    public string Name { get; set; }
    public string Description { get; set; }

    public SubjectViewModel() {
        Name = string.Empty;
        Description = string.Empty;
    }
  
}

