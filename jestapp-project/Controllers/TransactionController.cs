using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jestapp_project.Controllers
{
    public class TransactionController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public List<Transaction> Get(int UserID)
        {
            //לסיים שליפת רשימת טרנזקציות
           Transaction transaction = new Transaction();
            return transaction.GetTransactions(UserID);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Transaction transaction)
        {

            try
            {
                transaction.InsertTransaction(transaction);
                string msg = "Transaction Approved Successesfully";
                return Request.CreateResponse(HttpStatusCode.OK, msg);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

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