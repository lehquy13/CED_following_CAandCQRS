using FluentResults;

namespace CED.Application.Common.Errors.Users;

public class NonExistUserError : IError
{
    public string Message { get; init; } = "This user doesn't exist!";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}
