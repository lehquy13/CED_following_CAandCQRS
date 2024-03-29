using FluentResults;

namespace CED.Application.Common.Errors.ClassInformations;

public class RequestedClassError : IError
{
    public string Message { get; init; } = "Tutor has already requested this class!";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}