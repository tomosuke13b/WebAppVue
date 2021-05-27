using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
    public class ImageController : Controller
    {
        private ImageModel _model;

        public ImageController(WebAppContext context)
        {
            this._model = new ImageModel(context);
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Image>> Get()
        {
            return NotFound();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Image> Get(Int64 id)
        {
            return this._model.Get(id);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Image> Post(Image value)
        {
            var ret = this._model.Create(value);
            if (ret == null) return NotFound();
            return ret;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<long> Put(Int64 id, Image value)
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
