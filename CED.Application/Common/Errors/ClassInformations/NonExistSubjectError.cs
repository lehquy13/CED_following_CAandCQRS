using FluentResults;

namespace CED.Application.Common.Errors.ClassInformations;

public class NonExistSubjectError : IError
{
    public string Message { get; init; } = "This subject doesn't exist!";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}
