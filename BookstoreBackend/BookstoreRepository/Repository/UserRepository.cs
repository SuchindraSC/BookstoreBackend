using BookstoreModel;
using BookstoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using StackExchange.Redis;
using System.Security.Claims;

namespace BookstoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        string connectionString;
        string secretKey;
        EmailService service;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDbConnection");
            secretKey = configuration["SecretKey"];
            service = new EmailService(configuration);
        }

        public static string EncryptPass(string password)
        {
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encode);
        }

        public RegisterModel Register(RegisterModel user)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spUserRegisteration";
                    SqlCommand command = new SqlCommand(spName, connection);

                    user.password = EncryptPass(user.password);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fullName", user.fullName);
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@password", user.password);
                    command.Parameters.AddWithValue("@phone", user.phone);
                    command.Parameters.Add("@user", SqlDbType.Int).Direction = ParameterDirection.Output;
                    connection.Open();
                    command.ExecuteNonQuery();

                    var id = command.Parameters["@user"].Value;

                    if (!(id is DBNull))
                    {
                        user.CustomerId = Convert.ToInt32(id);
                        return user;
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

        public string Login(LoginModel user)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spUserLogin";
                    SqlCommand command = new SqlCommand(spName, connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@password", EncryptPass(user.password));
                    command.Parameters.Add("@user", SqlDbType.Int).Direction = ParameterDirection.Output;
                    connection.Open();

                    command.ExecuteNonQuery();

                    var id = command.Parameters["@user"].Value;

                    if (!(id is DBNull))
                    {
                        if (Convert.ToInt32(id) == 2)
                        {
                            GetUserDetails(user.email);
                            return "Login Successful";
                        }
                        return "Incorrect Password";
                    }
                    return "Login Failed, User Doesnot Exists";
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

        public void GetUserDetails(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = "SELECT * FROM Users WHERE email = @email";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@email", email);
                    connection.Open();

                    RegisterModel customer = new RegisterModel();
                    SqlDataReader rd = command.ExecuteReader();

                    if (rd.Read())
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "FullName", rd.GetString("fullName"));
                        database.StringSet(key: "Phone", rd.GetString("phone"));
                        database.StringSet(key: "UserId", rd.GetInt32("CustomerId").ToString());
                    }
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

        public string GenerateToken(string userName)
        {
            byte[] key = Convert.FromBase64String(secretKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, userName)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public string ForgotPassword(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spUserForgot";
                    SqlCommand command = new SqlCommand(spName, connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.Add("@user", SqlDbType.Int).Direction = ParameterDirection.Output;
                    connection.Open();

                    command.ExecuteNonQuery();

                    var id = command.Parameters["@user"].Value;

                    if (!(id is DBNull))
                    {
                        string token = this.GenerateToken(email);

                        // Connection to Redis Server
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();

                        // Set values to the Redis cache
                        database.StringSet(key: Convert.ToInt32(id).ToString(), token);

                        string msgBody = "You are receiving this mail because you(or someone else) have requested the reset of the password for your account.\n\n" +
                                    "Please click on the following link, or paste this into your browser to complete the process:\n\n" +
                                    "http://localhost:4200/resetpassword/" + $"{token}/{Convert.ToInt32(id).ToString()}";


                        this.service.SendMailSmtp(email, msgBody);
                        return "Email has been sent";
                    }
                    return "User Doesnot Exists";
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

        public string ResetPassword(ResetModel user)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Connection to Redis Server
                ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                IDatabase database = connectionMultiplexer.GetDatabase();

                string token = database.StringGet(user.CustomerId.ToString());

                if (token == user.token)
                {
                    // Update
                    using (connection)
                    {
                        string spName = "spUserReset";
                        SqlCommand command = new SqlCommand(spName, connection);


                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", user.CustomerId);
                        command.Parameters.AddWithValue("@password", EncryptPass(user.password));
                        connection.Open();

                        command.ExecuteNonQuery();
                    }

                    database.KeyDelete(user.CustomerId.ToString());

                    // Save all changes to the database
                    return "Reset Password Successful";
                }

                return "Token Expired";
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
