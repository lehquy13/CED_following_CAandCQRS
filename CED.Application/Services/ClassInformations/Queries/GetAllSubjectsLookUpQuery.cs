using CED.Contracts.ClassInformations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CED.Application.Services.ClassInformations.Queries;

public record GetAllSubjectsLookUpQuery(HttpContext HttpContext) : IRequest<List<SubjectLookupDto>>;


