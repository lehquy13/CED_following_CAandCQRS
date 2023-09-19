using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CED.Infrastructure.Services;

public class CloudinarySetting
{
    public const string _SectionName  = "CloudinarySettings";
    public string CloudName { get; init; } = null!;
    public string ApiKey { get; init; } = null!;
    public string ApiSecret { get; init; }= null!;
}

    