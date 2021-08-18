using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jestapp_project.Controllers
{
    public class LockersController : ApiController
    {
        // GET api/<controller>
        public List<Lockers> Get(int StationID )
        {

            Lockers lockers = new Lockers();
            return lockers.GetEmptyLocker( StationID );
        }

        [Route ("api/Lockers/{PackageID}")]
        // GET api/<controller>/5
        public List<Lockers> GetLocker(int PackageID)
        {
            Lockers lockers = new Lockers();
            return lockers.GetLocker(PackageID);

        }

        // POST api/<controller>
        public void Post()
        {

        }

        // PUT api/<controller>/5
        public void Put(Lockers lockerdetails)
        {
             lockerdetails.UpdateLocker();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}