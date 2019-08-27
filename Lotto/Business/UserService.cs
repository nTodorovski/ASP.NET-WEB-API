using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Data;
using DomainModels;
using DomainModels.Enum;
using Mappers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Business
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly JwtSettings _jwtSettings;

        public UserService(IRepository<User> userRepository,IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public void Register(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Username))
                throw new Exception("Username is required.");

            if (string.IsNullOrEmpty(model.FirstName))
                throw new Exception("FirstName is required.");

            if (string.IsNullOrEmpty(model.LastName))
                throw new Exception("LastName is required.");

            var md5 = new MD5CryptoServiceProvider();
            var passwordBytes = Encoding.ASCII.GetBytes(model.Password);
            var hashBytes = md5.ComputeHash(passwordBytes);
            var hash = Encoding.ASCII.GetString(hashBytes);

            var user = new User
            {
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = hash,
                Role = RoleEnum.Player,
                Balance = 1000
            };

            _userRepository.Add(user);
        }

        public UserModel Authenticate(LoginModel model)
        {
            var md5 = new MD5CryptoServiceProvider();
            var passwordBytes = Encoding.ASCII.GetBytes(model.Password);
            var hashBytes = md5.ComputeHash(passwordBytes);
            var hash = Encoding.ASCII.GetString(hashBytes);

            var user = _userRepository.GetAll()
                .FirstOrDefault(x => x.Password == hash && x.Username == model.Username);

            if (user == null)
                throw new Exception("Username or password is wrong.");

            //Create token
            var keyBytes = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var jwtTokenHandler = new JwtSecurityTokenHandler();


            var descriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role == RoleEnum.Admin ? "Administrator" : "Player")
                })
            };

            var token = jwtTokenHandler.CreateToken(descriptor);
            var tokenString = jwtTokenHandler.WriteToken(token);

            UserModel userModel = user.ToModel();
            userModel.Token = tokenString;
            return userModel;
        }
    }
}
