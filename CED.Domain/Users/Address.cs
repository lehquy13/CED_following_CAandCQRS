namespace CED.Domain.Users;

public class Address
{
    public string City { get; set; } = String.Empty;
    public string CityId { get; set; } = String.Empty;
    public string District { get; set; } = String.Empty;
    public string DistrictId { get; set; } = String.Empty;
    public string Ward { get; set; } = String.Empty;
    public string WardId { get; set; } = String.Empty;
    public string WardLevel { get; set; } = String.Empty;
    public string? EnglishName { get; set; } = String.Empty;
};

public class City
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
}

public class District
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string CityId { get; set; } = String.Empty;
    public string? EnglishName { get; set; } = String.Empty;
}

public class Ward
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string WardLevel { get; set; } = String.Empty;
    public string DistrictId { get; set; } = String.Empty;
}