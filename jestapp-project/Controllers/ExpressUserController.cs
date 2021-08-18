using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jestapp_project.Controllers
{
    public class ExpressUserController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody]ExpressUser UserDetails)
        {
            ExpressUser AddExpressUser = new ExpressUser();
            return AddExpressUser.PostUser(UserDetails);
        }

        public void Put(ExpressUser ExPackage)
        {
            ExPackage.UpdateExPackage(ExPackage);

        }
        [HttpPut]
        [Route("api/ExpressUser/{UpdateStatusDelivered}")]
        public int UpdateStatusDelivered(int PackageId)
        {
            ExpressUser express = new ExpressUser();
            return express.UpdateStatusDelivered(PackageId);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}