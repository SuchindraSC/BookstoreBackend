using BookstoreManager.Interface;
using BookstoreModel;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository repository;

        public AddressManager(IAddressRepository repository)
        {
            this.repository = repository;
        }

        public bool AddUserAddress(AddressModel userDetails)
        {
            try
            {
                return this.repository.AddUserAddress(userDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditAddress(AddressModel details)
        {
            try
            {
                return this.repository.EditAddress(details);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AddressModel> GetUserDetails(int userId)
        {
            try
            {
                return this.repository.GetUserDetails(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromUserDetails(int addressId)
        {
            try
            {
                return this.repository.RemoveFromUserDetails(addressId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
