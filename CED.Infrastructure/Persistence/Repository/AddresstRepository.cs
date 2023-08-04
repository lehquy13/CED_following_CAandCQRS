using CED.Domain.Users;
using CED.Infrastructure.Entity_Framework_Core;

namespace CED.Infrastructure.Persistence.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _appDbContext; 
    public AddressRepository(AppDbContext cEDDBContext)
    {
        _appDbContext = cEDDBContext;
    }
    

    public List<City> GetCities()
    {
        return _appDbContext.Cities.ToList();
    }

    public City? GetCity(string id)
    {
        return _appDbContext.Cities.FirstOrDefault(e => e.Id == id);
    }

    public List<District> GetDistricts()
    {
        return _appDbContext.Districts.ToList();

    }

    public District? GetDistrict(string id)
    {
        return _appDbContext.Districts.FirstOrDefault(e => e.Id == id);

    }

    public List<Ward> GetWards()
    {
        return _appDbContext.Wards.ToList();

    }

    public Ward? GetWard(string id)
    {
        return _appDbContext.Wards.FirstOrDefault(e => e.Id == id);

    }
}

