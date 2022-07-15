using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL

    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public UserRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;

        }

        public UserEntity Register(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = userRegistrationModel.Password;

                fundooContext.User.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string login(LoginModel login)
        {
            try
            {
                UserEntity user = new UserEntity();
                user = this.fundooContext.User.SingleOrDefault(x => x.Email == login.Email && x.Password == login.Password) ;
                if (user != null)
                {
                    var token = GenerateSecurityToken(user.Email, user.UserID);
                    return token ;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateSecurityToken(string email ,long id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration[("jwt:key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserID", id.ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        
    }
}
