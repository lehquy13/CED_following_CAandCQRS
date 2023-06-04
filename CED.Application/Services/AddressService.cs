using CED.Contracts;
using CED.Contracts.Interfaces.Services;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;
    public AddressService(IAddressRepository addressRepository, IMapper mapper)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
    }
    public AddressDto GetAddresses()
    {
        var cities = _mapper.Map<List<CityDto>>(_addressRepository.GetCities());
        var districts =  _mapper.Map<List<DistrictDto>>(_addressRepository.GetDistricts());
        var wards =  _mapper.Map<List<WardDto>>(_addressRepository.GetWards());

        foreach (var c in cities)
        {
            c.Districts.AddRange(districts.Where(r =>
            {

                return r.CityId == c.Id;
            }));
            foreach (var d in c.Districts)
            {
                d.Wards.AddRange(wards.Where(r => r.DistrictId == d.Id));
            }

            
        }

        return new AddressDto(cities);
    }

    public List<CityDto> GetCities()
    {
        return _mapper.Map<List<CityDto>>(_addressRepository.GetCities());
    }
}