using FluentResults;

namespace CED.Application.Common.Errors.Users;

public class NonExistTutorError : IError
{
    public string Message { get; init; } = "This tutor doesn't exist!";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new()
    {
        new NonExistUserError()
    };
}
