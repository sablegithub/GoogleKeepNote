using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public LabelController(ILabelBL labelBL,IMemoryCache memoryCache,IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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

        [HttpGet("All")]
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
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedlabelList;
            var labelList = new List<LabelEntity>();
            var redislabelList = await distributedCache.GetAsync(cacheKey);
            if (redislabelList != null)
            {
                serializedlabelList = Encoding.UTF8.GetString(redislabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedlabelList);
            }
            else
            {
                labelList = (List<LabelEntity>)labelBL.GetLables();
                serializedlabelList = JsonConvert.SerializeObject(labelList);
                redislabelList = Encoding.UTF8.GetBytes(serializedlabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redislabelList, options);
            }
            return Ok(labelList);
        }


    }
}
