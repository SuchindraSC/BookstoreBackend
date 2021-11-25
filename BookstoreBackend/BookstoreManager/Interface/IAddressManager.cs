using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Interface
{
    public interface IAddressManager
    {
        bool AddUserAddress(AddressModel userDetails);
        bool EditAddress(AddressModel details);
        List<AddressModel> GetUserDetails(int userId);
        bool RemoveFromUserDetails(int addressId);
    }
}
