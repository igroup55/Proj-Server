using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jestapp_project.Controllers
{
    public class TDUserController : ApiController
    {

        [Route("api/TDUser/{UpdatedRating}/{UserId}")]
        public List<TDUser> GetUpdatedRating(int UserId)
        {
            TDUser gur = new TDUser();
            return gur.GetUpdatedRating(UserId);
        }

        [Route("api/TDUser/{GetDeliveryId}")]
        public List<TDUser> GetDeliveryId(int UserId)
        {
            TDUser gur = new TDUser();
            return gur.GetDeliveryId(UserId);
        }

        public List<TDUser> GetTDUserList(int startStation, int endStation, float Pweight)
        {
            TDUser Interests = new TDUser();

            return Interests.GetTDUserList(startStation, endStation, Pweight);

        }

        public int GetPossiblePickup(int StartStation, int EndStation, int UserId)
        {
            TDUser IsInterested = new TDUser();

            return IsInterested.GetPossiblePickup(StartStation, EndStation, UserId);


        }

        public void Put(TDUser tDUser)
        {
            tDUser.UpdateTDUserPack();

        }

        // GET api/<controller>/5
        public TDUser Get(int UserId)
        {

            TDUser tDUser = new TDUser();
            return tDUser.GetInterestedTD(UserId);
        }

        [Route("api/TDUser/{Rating}")]
        public HttpResponseMessage PutRating(TDUser Category)
        {
            try
            {
                TDUser ctd = new TDUser();
               List<float> result = ctd.RatingAlgorithm(Category);


                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST api/<controller>
        public int Post([FromBody] TDUser tduser)
        {

            return tduser.AddTDUser();
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

       



        [Route("api/TDUser/{GetDate}")]
        public HttpResponseMessage GetDate(int StartStation, int EndStation, int UserId, DateTime PickUpDT)
        {
            try
            {
                TDUser ctd = new TDUser();
                List<TDUser> result = ctd.GetDate(StartStation, EndStation, UserId, PickUpDT);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}