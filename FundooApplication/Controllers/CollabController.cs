using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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

        [HttpGet("All")]
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
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedlabelList;
            var labelList = new List<CollabEntity>();
            var redislabelList = await distributedCache.GetAsync(cacheKey);
            if (redislabelList != null)
            {
                serializedlabelList = Encoding.UTF8.GetString(redislabelList);
                labelList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedlabelList);
            }
            else
            {
                labelList = (List<CollabEntity>)collabBL.GetCollabs();
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
