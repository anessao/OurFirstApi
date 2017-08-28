using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OurFirstApi.Models;

namespace OurFirstApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet] //the get attribute allows you to name the function whatever you want
        [Route("otherapi/zalues/")] //specify the template in the parameters
        public IEnumerable<string> NotBasedOnAnything()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int randomNumber)
        {
            return "value";
        }

        // POST api/values
        public HttpResponseMessage Post(EmployeeListResult employee)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, EmployeeListResult employee)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        // DELETE api/values/5
        [HttpDelete, Route("{id}")]
        public void Delete(int id)
        {
        }
    }
}
