using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooApplication.Controllers
{
    [Route("[controller]")]
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

        [HttpDelete]
        public IActionResult Delete(long NoteID)
        {
            long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);      
            var result = noteBL.Delete(NoteID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Note Delete Successful"});
            }
            else
            {
                return this.NotFound(new { success = false, message = "Note does not Delete" });
            }
        }

        [HttpGet]
        public IActionResult Retrieve(long NoteID)
        {
            long ID = Convert.ToInt32(User.Claims.All(x => x.Type == "UserID"));
            var result = noteBL.Retrieve(NoteID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "data Reterieve Successful", data = result });
            }
            else
            {
                return this.NotFound(new { success = false, message = "data not Reterieve" });
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateModel updateModel, long NoteID)
        {
            long ID = Convert.ToInt32(User.Claims.All(x => x.Type == "UserID"));
            var result=noteBL.Update( updateModel,NoteID);
            if(result != null)
            {
                return Ok(new { success = true, message ="Data Updated Successful",data=result});
            }
            else
            {
                return this.NotFound(new { success = false, message = "Data NotUpdated" });
            }
        }
    
    }
}
