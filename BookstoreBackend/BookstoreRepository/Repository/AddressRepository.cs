using BookstoreModel;
using BookstoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BookstoreRepository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        string connectionString;

        public AddressRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDbConnection");
        }

        public bool AddUserAddress(AddressModel userDetails)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spAddUserAddress";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    command.Parameters.AddWithValue("@userId", userDetails.UserId);
                    command.Parameters.AddWithValue("@address", userDetails.Address);
                    command.Parameters.AddWithValue("@city", userDetails.City);
                    command.Parameters.AddWithValue("@state", userDetails.State);
                    command.Parameters.AddWithValue("@type", userDetails.Type);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool EditAddress(AddressModel userDetails)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spUpdateUserDetails";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    command.Parameters.AddWithValue("@address", userDetails.Address);
                    command.Parameters.AddWithValue("@city", userDetails.City);
                    command.Parameters.AddWithValue("@state", userDetails.State);
                    command.Parameters.AddWithValue("@type", userDetails.Type);
                    command.Parameters.AddWithValue("@addressID", userDetails.AddressId);
                    command.Parameters.Add("@result", SqlDbType.Int);
                    command.Parameters["@result"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    var result = command.Parameters["@result"].Value;
                    if (result.Equals(1))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<AddressModel> GetUserDetails(int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetUserDetails";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader readData = command.ExecuteReader();
                    List<AddressModel> userdetaillist = new List<AddressModel>();
                    if (readData.HasRows)
                    {
                        while (readData.Read())
                        {
                            AddressModel userDetail = new AddressModel();
                            userDetail.AddressId = readData.GetInt32("AddressId");
                            userDetail.UserId = readData.GetInt32("UserId");
                            userDetail.Address = readData.GetString("address");
                            userDetail.City = readData.GetString("city").ToString();
                            userDetail.State = readData.GetString("state");
                            userDetail.Type = readData.GetString("type");
                            userdetaillist.Add(userDetail);
                        }
                    }
                    return userdetaillist;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool RemoveFromUserDetails(int addressId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spRemoveUserDetails";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@AddressId", addressId);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
