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
    public class BooksRepository : IBooksRepository
    {
        string connectionString;

        public BooksRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDbConnection");
        }

        public List<BookModel> GetAllBooks()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = "SELECT * FROM Books";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<BookModel> bookList = new List<BookModel>();
                        while (reader.Read())
                        {
                            BookModel book = new BookModel();
                            book.BookId = Convert.ToInt32(reader["BookId"]);
                            book.author = reader["author"].ToString();
                            book.bookName = reader["bookName"].ToString();
                            book.description = reader["description"].ToString();
                            book.bookImage = reader["bookImage"].ToString();
                            book.price = Convert.ToInt32(reader["price"]);
                            book.discountPrice = Convert.ToInt32(reader["discountPrice"]);
                            book.quantity = Convert.ToInt32(reader["quantity"]);
                            book.rating = Convert.ToDouble(reader["rating"]);
                            book.count = Convert.ToInt32(reader["count"]);
                            bookList.Add(book);
                        }
                        return bookList;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public BookModel GetBookDetails(int BookId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetBookDetails";
                    SqlCommand command = new SqlCommand(spName, connection);

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    command.Parameters.AddWithValue("@BookId", BookId);
                    SqlDataReader reader = command.ExecuteReader();
                    BookModel book = new BookModel();
                    
                    if (reader.Read())
                    {
                        book.BookId = Convert.ToInt32(reader["BookId"]);
                        book.author = reader["author"].ToString();
                        book.bookName = reader["bookName"].ToString();
                        book.description = reader["description"].ToString();
                        book.bookImage = reader["bookImage"].ToString();
                        book.price = Convert.ToInt32(reader["price"]);
                        book.discountPrice = Convert.ToInt32(reader["discountPrice"]);
                        book.quantity = Convert.ToInt32(reader["quantity"]);
                    }
                    return book;
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

        public List<FeedbackModel> GetCustomerFeedBack(int bookid)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetCustomerFeedback";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    command.Parameters.AddWithValue("@bookid", bookid);
                    List<FeedbackModel> feedbackList = new List<FeedbackModel>();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FeedbackModel feedbackdetails = new FeedbackModel();
                            feedbackdetails.userId = reader.GetInt32("CustomerId");
                            feedbackdetails.feedbackId = reader.GetInt32("feedbackId");
                            feedbackdetails.bookId = reader.GetInt32("bookId");
                            feedbackdetails.customerName = reader.GetString("FullName");
                            feedbackdetails.feedback = reader.GetString("Feedback");
                            feedbackdetails.rating = reader.GetDouble("Rating");
                            feedbackList.Add(feedbackdetails);
                        }

                    }
                    return feedbackList;
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
