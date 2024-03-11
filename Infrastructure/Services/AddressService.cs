using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class AddressService
    {
        private readonly AddressRepository _addressRepository;
        public AddressService(AddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressEntity> CreateAddress(AddressEntity newAddress)
        {
            try
            {
                if (!await _addressRepository.Exists(x => x.AddressLine_1 == newAddress.AddressLine_1 && x.City == newAddress.City))
                {
                    var result = await _addressRepository.AddToDB(newAddress);
                    if (result != null)
                        return newAddress;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
        public async Task<AddressEntity> GetOneAddress(AddressEntity address)
        {
            try
            {
                if (await _addressRepository.Exists(x => x.AddressLine_1 == address.AddressLine_1 && x.City == address.City))
                {
                    var result = await _addressRepository.GetOneFromDB(x => x.AddressLine_1 == address.AddressLine_1 && x.City == address.City);

                    if (result != null)
                        return result;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<AddressEntity> GetOneAddressById(int id)
        {
            try
            {
                if (await _addressRepository.Exists(x => x.Id == id))
                {
                    var result = await _addressRepository.GetOneFromDB(x => x.Id == id);

                    if (result != null)
                        return result;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<AddressEntity> UpdateAddress(AddressEntity newValues)
        {
            try
            {
                if (await _addressRepository.Exists(a => a.AddressLine_1 == newValues.AddressLine_1 && a.City == newValues.City))
                {
                    var updated = await GetOneAddress(newValues);
                    if (updated != null) return updated;
                }
                else
                {
                    var createdAddress = await CreateAddress(newValues);
                    if (createdAddress != null)
                        return createdAddress;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}

