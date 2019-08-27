using DomainModels;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IUserService
    {
        void Register(RegisterModel model);
        IEnumerable<User> GetAll();
        UserModel Authenticate(LoginModel model);
    }
}
