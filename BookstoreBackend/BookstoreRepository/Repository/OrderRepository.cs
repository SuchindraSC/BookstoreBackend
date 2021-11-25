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
    public class OrderRepository : IOrderRepository
    {
        string connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDbConnection");
        }

        public bool PlaceTheOrder(List<Order> orderdetails)
        {
            bool res = false;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    foreach (var order in orderdetails)
                    {
                        string spName = "spPlaceOrder";
                        SqlCommand command = new SqlCommand(spName, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.AddWithValue("@BookId", order.BookId);
                        command.Parameters.AddWithValue("@UserId", order.UserId);
                        command.Parameters.AddWithValue("@QuantityToBuy", order.QuantityToBuy);
                        string date = DateTime.Now.ToString(" dd MMM yyyy");
                        command.Parameters.AddWithValue("@OrderDate", date);
                        var returnedSQLParameter = command.Parameters.Add("@result", SqlDbType.Int);
                        returnedSQLParameter.Direction = ParameterDirection.Output;
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            res = true;
                            connection.Close();
                        }
                        else
                        {
                            res = false;
                            break;
                        }
                    }
                    return res;
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

        public List<OrderModel> GetOrderList(int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetOrder";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    command.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<OrderModel> orderList = new List<OrderModel>();
                        while (reader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            OrderModel orderModel = new OrderModel();

                            orderModel.BookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.author = reader["author"].ToString();
                            bookModel.bookName = reader["bookName"].ToString();
                            bookModel.description = reader["description"].ToString();
                            bookModel.price = Convert.ToInt32(reader["price"]);
                            bookModel.discountPrice = Convert.ToInt32(reader["discountPrice"]);
                            bookModel.bookImage = reader["bookImage"].ToString();
                            orderModel.OrderId = Convert.ToInt32(reader["OrderId"]);
                            orderModel.UserId = Convert.ToInt32(reader["UserId"]);
                            orderModel.QuantityToBuy = Convert.ToInt32(reader["QuantityToBuy"]);
                            orderModel.DateOfOrder = reader["DateOfOrder"].ToString();
                            orderModel.Books = bookModel;

                            orderList.Add(orderModel);
                        }
                        return orderList;
                    }
                    else
                    {
                        return null;
                    }
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
