using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IUserService
    {
        void Register(UserModel model);
    }
}
