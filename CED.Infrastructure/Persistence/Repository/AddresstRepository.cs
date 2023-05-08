using CED.Domain.Users;
using CED.Infrastructure.Entity_Framework_Core;

namespace CED.Infrastructure.Persistence.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly CEDDBContext _ceddbContext; 
    public AddressRepository(CEDDBContext cEDDBContext)
    {
        _ceddbContext = cEDDBContext;
    }
    

    public List<City> GetCities()
    {
        return _ceddbContext.Cities.ToList();
    }

    public City? GetCity(string id)
    {
        return _ceddbContext.Cities.FirstOrDefault(e => e.Id == id);
    }

    public List<District> GetDistricts()
    {
        return _ceddbContext.Districts.ToList();

    }

    public District? GetDistrict(string id)
    {
        return _ceddbContext.Districts.FirstOrDefault(e => e.Id == id);

    }

    public List<Ward> GetWards()
    {
        return _ceddbContext.Wards.ToList();

    }

    public Ward? GetWard(string id)
    {
        return _ceddbContext.Wards.FirstOrDefault(e => e.Id == id);

    }
}

