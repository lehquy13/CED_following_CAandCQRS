﻿using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;

public interface IAddressRepository 
{
    List<City> GetCities();
    City? GetCity(string id);
    List<District> GetDistricts();
    District? GetDistrict(string id);

    List<Ward> GetWards();
    Ward? GetWard(string id);

    
}

