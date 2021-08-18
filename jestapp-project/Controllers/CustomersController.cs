using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jestapp_project.Controllers
{
    public class CustomersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public List<Customers> GetExpressCustomerList(int UserID, double Elat, double Elng, int Station)
        {
            Customers stations = new Customers();
            return stations.GetExpressCustomerList(UserID, Elat, Elng, Station);
        }
        // GET api/<controller>/5
        public Customers GetCustDetails(int PackageId)
        {
            Customers CustDetails = new Customers();
            return CustDetails.GetCustDetails(PackageId);
        }

        // POST api/<controller>
        public void Post([FromBody]Customers customers)
        {
           customers.AddCust();


        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
     
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}