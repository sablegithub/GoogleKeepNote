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
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;

        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }

        [HttpPost("Create")]
        public IActionResult Create(CollabModel collabModel,long noteid)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            var result = collabBL.Create(collabModel, userid,noteid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Collab is Create Successful", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Collab is not Create UnSuccessful" });
            }
        }

        [HttpGet("Retrieve All")]
        public IActionResult GetCollab()
        {
            
            var result = collabBL.GetCollabs();
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Retrieve Successful",data=result });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Collab is not Retrieve" });
            }
        }

        [HttpDelete]
        public IActionResult Delete(long NoteID)
        {
            long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            var result = collabBL.Delete(NoteID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Collab Delete Successful" });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Collab does not Delete" });
            }
        }
    }
}
