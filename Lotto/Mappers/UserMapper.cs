using DomainModels;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mappers
{
    public static class UserMapper
    {
        public static UserModel ToModel(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
