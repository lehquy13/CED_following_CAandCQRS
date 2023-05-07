using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;

public interface IAddressRepository 
{
    List<City> GetCities();
    List<District> GetDistricts();
    List<Ward> GetWards();
}

