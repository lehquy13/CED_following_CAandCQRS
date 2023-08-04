using FluentResults;

namespace CED.Application.Common.Errors.ClassInformations;

public class NonExistRequestError : IError
{
    public string Message { get; init; } = "This request in the class doesn't exist!";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}
