using Cloocks.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloocks.Server.Controllers
{
    [Route("api/[controller]")]
    public class WatchesController : Controller
    {
        private readonly MongoRepository<Clock> clocks = new MongoRepository<Clock>();

        // GET api/values
        [HttpGet]
      //  [Route("api/items/get/{page}")]
        public async Task<IActionResult> GetAsync(int page)
        {
            var res3 = await clocks.GetSkip(page);
            return Ok(res3);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
