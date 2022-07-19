﻿using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Register(UserRegistrationModel userRegistrationModel);

        public string login(LoginModel loginModel);

        public string ForgetPassword(string EmailID);

        public string ResetPassWord(ResetPassword resetPassword);

    }
}
