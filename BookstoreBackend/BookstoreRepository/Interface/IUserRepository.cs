using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.Interface
{
    public interface IUserRepository
    {
        public RegisterModel Register(RegisterModel user);
        public string Login(LoginModel user);
        public string GenerateToken(string userName);
        public string ForgotPassword(string email);
        public string ResetPassword(ResetModel user);
    }
}
