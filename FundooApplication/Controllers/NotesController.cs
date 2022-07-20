using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL noteBL;

        public NotesController(INotesBL noteBL)
        {
            this.noteBL = noteBL;
        }


        [HttpPost("Create")]
        public IActionResult CreateNote(NotesModel notesModel)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            var result = noteBL.Create(notesModel,userid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Note Create Successful", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note Create UnSuccessful" });
            }

        }

    }
}
