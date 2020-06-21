using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppVue.Models;
using WebAppVue.Models.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppVue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            // return new string[] { "value1", "value2" };
            var model = new NameModel();
            return model.List();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Item> Get(Int64 id)
        {
            var model = new NameModel();
            return model.Get(id);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(Item value)
        {
            var model = new NameModel();
            if (!model.Create(value)) return NotFound();
            return NoContent();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(Int64 id, Item value)
        {
            var model = new NameModel();
            if (!model.Update(id, value)) return NotFound();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Int64 id)
        {
            var model = new NameModel();
            if (!model.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
