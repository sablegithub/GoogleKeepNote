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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;

        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL; 
        }


        [HttpPost("Create")]
        public IActionResult Create(LabelModel labelModel, long noteid)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            var result = labelBL.Create(labelModel, userid, noteid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "label  Create Successful", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "label is not Created" });
            }
        }
        [HttpDelete]
        public IActionResult Delete(long LabelID)
        {
            long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            var result = labelBL.Delete(LabelID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Label Delete Successful" });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Label does not Delete" });
            }
        }
        [HttpPut("Update")]
        public IActionResult Update(LabelModel labelModel, long ID)
        {
            long data = Convert.ToInt32(User.FindFirst(x => x.Type == "UserID").Value);
            var result =labelBL.Update(labelModel, ID);
            if(result != null)
            {
                return Ok(new { success = true, message = "Data Updated Successful", data = result });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Data Not Updated" });
            }
        }

        [HttpGet("Retrieve All")]
        public IActionResult GetNotes()
        {
            // public IEnumerable<NotesEntity> GetNotess()
            var result = labelBL.GetLables();
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Retrieve Successful", data = result });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Notes is not Retrieve" });
            }
        }


    }
}
