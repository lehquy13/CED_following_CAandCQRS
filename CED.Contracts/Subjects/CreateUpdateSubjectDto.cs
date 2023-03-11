using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CED.Contracts.Entities.Subject;

public class CreateUpdateSubjectDto : IRequest<bool>
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateUpdateSubjectDto() {
        Name = string.Empty;
        Description = string.Empty;
    }
}

