using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jestapp_project.Controllers
{
    public class StationsController : ApiController
    {

        // GET api/<controller>/5
        public List<Stations> GetAllStations()
        {

            Stations stations = new Stations();
            return stations.GetStations();

        }


        public List<Stations> GetStationCoords(int stationID)
        {
            Stations stations = new Stations();
            return stations.GetStationCoords(stationID);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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