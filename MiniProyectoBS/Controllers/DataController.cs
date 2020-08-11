using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BL;

namespace MiniProyectoBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        IServiceData _serviceData = null;

        public DataController(IServiceData serviceData)
        {
            _serviceData = serviceData;
        }

        // GET: api/Data
        [HttpGet]
        public async Task<JsonResult> GetAsync()
        {
            var albums = await _serviceData.GetAlbumsAsync();
            var photos = await _serviceData.GetPhotos();
            var comments = await _serviceData.GetComments();
            return new JsonResult(new { albums, photos, comments });
        }

        // GET: api/Data/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Data
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Data/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
