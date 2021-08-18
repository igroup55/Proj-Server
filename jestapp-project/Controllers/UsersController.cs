using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using jestapp_project.Models;

namespace jestapp_project.Controllers
{
    public class UsersController : ApiController
    {
    
        public List<Users> Get(string email, string pass)
        {
            Users user = new Users();
            return user.LoginUser(email, pass);

        }

        public HttpResponseMessage Post([FromBody]Users users)
        {

            try
            {
                users.InsertUser();
                string msg = "Inserted Successesfully";
                return Request.CreateResponse(HttpStatusCode.OK, msg);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [Route("api/Users/{CheckExistUser}/{UserId}")]
        public List<Users> Get_CheckExistUser(int UserId)
        {
            Users user = new Users();
            return user.CheckExistUser(UserId);

        }
        [Route("api/Users/{GetSenderToken}")]
        public Users GetSenderToken(int UserId, int PackageID)
        {
            Users user = new Users();
            return user.GetSenderToken(UserId, PackageID);

        }
        [Route("api/Users/{UpdateUserToken}")]
        public void Put(int userId, string token)
        {
            Users users = new Users();
            users.UpdateUserToken(userId, token);
        }

        public void Put(Users users)
        {
            users.UpdateDT();
        }

    }
}