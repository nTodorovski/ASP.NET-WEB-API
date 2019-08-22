using System;
using System.Collections.Generic;
using System.Text;
using Data;
using DomainModels;
using DomainModels.Enum;
using Models;

namespace Business
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(UserModel model)
        {
            if (string.IsNullOrEmpty(model.Username))
                throw new Exception("Username is required.");

            if (string.IsNullOrEmpty(model.FirstName))
                throw new Exception("FirstName is required.");

            if (string.IsNullOrEmpty(model.LastName))
                throw new Exception("LastName is required.");


            var user = new User
            {
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Balance = 1000
            };

            if (model.Username == "Risto")
            {
                user.Role = RoleEnum.Admin;
            }
            else
            {
                user.Role = RoleEnum.Player;
            }

            _userRepository.Add(user);
        }
    }
}
