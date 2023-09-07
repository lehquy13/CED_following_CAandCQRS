using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CED.Application.Services.Users.Commands;

public record ChangeAvatarCommand
(
    string Id,
    IFormFile? File
    ) : IRequest<Result<string>>;

