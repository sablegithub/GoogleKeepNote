using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
   
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        

        [HttpPost("Register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            var result = userBL.Register(userRegistrationModel);
            if(result != null )
            {
                return this.Ok(new { success = true, message = "Registration Successful", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Registration UnSuccessful" });
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
       
    }

}
