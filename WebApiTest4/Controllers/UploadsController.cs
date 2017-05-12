using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest4.Controllers
{
    public class UploadsController : ApiController
    {
        // GET: api/Uploads
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Uploads/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Uploads
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Uploads/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Uploads/5
        public void Delete(int id)
        {
        }
    }
}
