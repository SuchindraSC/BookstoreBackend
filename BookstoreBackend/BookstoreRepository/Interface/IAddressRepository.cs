using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.Interface
{
    public interface IAddressRepository
    {
        bool AddUserAddress(AddressModel userDetails);
        bool EditAddress(AddressModel details);
        List<AddressModel> GetUserDetails(int userId);
        bool RemoveFromUserDetails(int addressId);
    }
}
