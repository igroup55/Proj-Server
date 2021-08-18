using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using jestapp_project.Models;

namespace jestapp_project.Controllers
{
    public class PackagesController : ApiController
    {
        // GET api/<controller> 
        public List<Packages> Get(int UserID)
        {
            Packages packages = new Packages();
            return packages.GetPackageID(UserID);
        }

        public List<Packages> GetPackageList(int startStation, int endStation , float Pweight , bool express)
        {
            Packages package = new Packages();
            
            return package.GetPackageList(startStation, endStation , Pweight, express);

        }

        public List<Packages> GetPrice(int PackageId)
        {
            Packages packages = new Packages();
            return packages.GetPrice(PackageId);
        }

        [Route("api/Packages/{MYPackageID}")]
        public Packages GetSenderId(int PackageId)
        {
            Packages packages = new Packages();
            return packages.GetSenderId(PackageId);
        }

        // POST api/<controller>
        public int Post([FromBody]Packages packages)
        {
           
           return packages.AddPack();
        }

        [Route("api/Packages/{UpdatePrice}")]
        public void Put_UpdatePrice(Packages packages)
        {
            packages.UpdatePrice();
        }


        [Route("api/Packages/{UpdatePackLocker}/{Locker}")]
        public void PutUpdatePackLocker(Packages packages)
        {
            packages.UpdateLocker();

        }


        // PUT api/<controller>/5
        public void Put(Packages packages)
        {
            packages.UpdatePackage();

        }

        public List<Packages> GetPackagesByStations(int startStation, int endStation)
        {
            Packages package = new Packages();
            return package.GetPackagesByStations(startStation, endStation);
        }


       
        public List<Packages> GetCanceledPackages()
        {
            Packages PackageLockers = new Packages();
            return PackageLockers.GetCanceledPackages();
        }
    }
}