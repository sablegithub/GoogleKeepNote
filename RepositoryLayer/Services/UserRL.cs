using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
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
                userEntity.Password = convertoencrypt(userRegistrationModel.Password);

                fundooContext.UserTable.Add(userEntity);
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
                user = this.fundooContext.UserTable.SingleOrDefault(x => x.Email == login.Email);
                string result = convertoDecrypt(user.Password);
                if (user != null && result==login.Password)
                {
                    var token = GenerateSecurityToken(user.Email, user.UserID);
                    return token;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string key = "adef@@kfxcbv@";
        public static string convertoencrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public static string convertoDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base32EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base32EncodeBytes);
            result = result.Substring(0, result.Length - key.Length);
            return result;
        }


        public string GenerateSecurityToken(string email, long id)
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
        public string ForgetPassword(string EmailID)
        {
            var emailcheck = fundooContext.UserTable.SingleOrDefault(x => x.Email == EmailID);
            if (emailcheck != null)
            {
                var token = GenerateSecurityToken(emailcheck.Email, emailcheck.UserID);
                MSMQ mSMQ = new MSMQ();
                mSMQ.sendData2Queue(token);
                return token;
            }
            else
            {
                return null;
            }
        }

        public string ResetPassWord(ResetPassword resetPassword)
        {

            // var Passcheck = fundooContext.User.SingleOrDefault(x =>x.Password == resetPassword);

            try
            {
                if (resetPassword.NewPassword == resetPassword.ConformPassword)
                {
                    UserEntity userEntity = fundooContext.UserTable.Where(x => x.Email == resetPassword.Email).FirstOrDefault();
                    userEntity.Password = resetPassword.ConformPassword;
                    fundooContext.SaveChanges();
                    return "Reset Successfully";
                }
                else
                {
                    return "Reset Failed";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }   
}
