using CED.Application.Services.Subjects.Queries;
using CED.Contracts.Subjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CED.Web.Pages.Subjects
{
    public class IndexModel : PageModel
    {
        //dependencies 
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public List<SubjectDto> subjectDtos { get; set; } = new List<SubjectDto>();

        public IndexModel(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task OnGetAsync()
        {
            var query = new GetAllSubjectsQuery();
            subjectDtos = await _mediator.Send(query);
           
        }
    }
}
