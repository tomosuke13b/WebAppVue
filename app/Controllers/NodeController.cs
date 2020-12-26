using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppVue.Models;
using WebAppVue.Models.Entity;
using WebAppVue.Models.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppVue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : Controller
    {
        private NodeModel _model;

        public NodeController(WebAppContext context)
        {
            this._model = new NodeModel(context);
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            // return new string[] { "value1", "value2" };
            return this._model.List();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Item> Get(Int64 id)
        {
            return this._model.Get(id);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<long> Post(Item value)
        {
            var ret = this._model.Create(value);
            if (ret == -1) return NotFound();
            return ret;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<long> Put(Int64 id, Item value)
        {
            var ret = this._model.Update(id, value);
            if (ret == -1) return NotFound();
            return ret;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(Int64 id)
        {
            var ret = this._model.Delete(id);
            if (!ret) return NotFound();
            return ret;
        }
    }
}
