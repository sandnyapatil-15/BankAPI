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
    [Route("api/AdminAPI")]
    public class AdminAPIController : ApiController
    {
        BankEntities db = new BankEntities();

        /*[Route("api/AdminAPI/Login/{name}/{pwd}")]
        [HttpGet]
        public string Get(int uid, string pwd)
        {
            string result = "";
            try
            {
                var data = db..Where(x => x.PSNo == uid   && x.Admin_Password == pwd );
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
        }*/

        [Route("api/AdminAPI/GetAllRegistrations")]

        [HttpGet]
        public IEnumerable<User_Details> Get()
        {
            try
            {
                var data = db.User_Details.Where(x => x.Status == "Waiting for Approval");

                return data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

      /*  [Route("api/AdminAPI/UpdateStatus/{id}")]
        [HttpPut]
        public bool Put(int id, [FromBody] User_Details newsts)
        {
            try
            {
                var olddata = db.User_Details.Where(x => x.Acc_No == id).SingleOrDefault();
                if (olddata == null)
                    throw new Exception("Invalid Data");
                else
                {
                    olddata.A_Status = .EmpID;
                    olddata.EmpName = newemp.EmpName;
                    olddata.Desg = newemp.Desg;
                    olddata.Dept = newemp.Dept;
                    olddata.Salary = newemp.Salary;
                    olddata.password = newemp.password;
                    olddata.projid = newemp.projid;
                    var res = db.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }*/
        

    }
}
