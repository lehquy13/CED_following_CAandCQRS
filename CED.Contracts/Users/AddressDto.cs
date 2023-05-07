namespace CED.Contracts.Users;

public record AddressDto(List<CityDto> Cities);

public class CityDto
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;

    public List<DistrictDto> Districts { get; set; } = new List<DistrictDto>();
}

public class DistrictDto
{
    public string Id { get; set; } = String.Empty;
    public List<WardDto> Wards { get; set; } = new List<WardDto>();
    public string? EnglishName { get; set; } = String.Empty;
}

public class WardDto
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
}