using CED.Contracts.Users;

namespace CED.Contracts.Interfaces.Services;

public interface IAddressService
{
    AddressDto GetAddresses();
}