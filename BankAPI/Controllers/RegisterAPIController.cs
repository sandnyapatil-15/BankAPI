using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankAPI.Models;
using System.Web.Http.Cors;

namespace BankAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/RegisterAPI")]
    public class RegisterAPIController : ApiController
    {
        BankEntities db = new BankEntities();
        [Route("api/RegisterAPI/RegisterCustomer")]
        [HttpPost]
         public bool Post([FromBody] User_Details ud)
        {
            try
            {
                db.User_Details.Add(ud);
                var res = db.SaveChanges();
                if (res > 0)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        [Route("api/RegisterAPI/Login/{name}/{pwd}")]
        [HttpGet]
        public string Get(string name, string pwd)
        {
            string result =null;
            try
            {
                var data = db.UserAccounts.Where(x => x.User_Id == name && x.Login_Password == pwd);
                if (data.Count() == 0)
                    result = "Invalid Credentials";
                else
                    result = "Login Successfull";
            }
            catch (Exception e)
            {
                throw e;
            }
            //Console.Write(result);
            return result;
        }
    }
}
