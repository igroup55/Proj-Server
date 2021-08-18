using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jestapp_project.Controllers
{
    public class UserCreditsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public List<UserCredits> Get(int UserID)
        {
            UserCredits user = new UserCredits();
            return user.GetUserCredit(UserID);

        }

        [HttpGet]
        [Route("api/UserCredits/{TDUserID}")]
        public List<UserCredits> GetTD(int TDUserID)
        {
            UserCredits user = new UserCredits();
            return user.GetTDUsers(TDUserID);

        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]UserCredits userCredits)
        {

            try
            {
                userCredits.InsertUserCredits(userCredits);
                string msg = "Transaction Approved Successesfully";
                return Request.CreateResponse(HttpStatusCode.OK, msg);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        // PUT api/<controller>/5
        public void Put(UserCredits userCredits)
        {
            userCredits.UpdateUserCredits(userCredits);

        }
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}