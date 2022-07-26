using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;


namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;
            
        }
       
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = userBL.Register(userRegistrationModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                   // throw new Exception("Error Occured");
                    return this.BadRequest(new { success = false, message = "Registration UnSuccessful" });
                }
                
            }
            catch(Exception ex)
            {
                 logger.LogError(ex.ToString());
                 return BadRequest(ex.ToString());
               
            }

        }

        [HttpPost("Login")]
        public IActionResult login(LoginModel loginModel)
        {
            var result = userBL.login(loginModel);
            if ( result != null)
            {
                return this.Ok(new { success = true, message = "Login Successful",data= result});
            }
            else
            {
                return this.NotFound(new { success = false, message = "Login UnSuccessful" });
            }
        }

        [HttpPost("Forget")]
        public IActionResult Forget(string EmailID)
        {
            var result = userBL.ForgetPassword(EmailID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Mail Send is  Successful"});
            }
            else
            {
                return this.NotFound(new { success = false, message = "Mail Send is  UnSuccessful" });
            }
        }

        [Authorize]
        [HttpPost("Reset")]
        public IActionResult ResetPassWord(ResetPassword resetPassword)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var result = userBL.ResetPassWord(resetPassword);

            if (result != null)
            {
                return this.Ok(new { success = true, message = "Reset Successfully" });
            }
            
            else
            {
                return this.Unauthorized(new { success = false, message = "Reset Failed" });
            }
        }
    }

}
