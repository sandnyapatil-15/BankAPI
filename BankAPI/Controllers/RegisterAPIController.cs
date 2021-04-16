using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankAPI.Models;
using System.Web.Http.Cors;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;

namespace BankAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/RegisterAPI")]
    public class RegisterAPIController : ApiController
    {
        BankEntities db = new BankEntities();
        [Route("api/RegisterAPI/RegisterCustomer")]
        [HttpPost]
         public int Post([FromBody] User_Details ud)
        {
            try
            {
                db.User_Details.Add(ud);
                var res = db.SaveChanges();
                if (res > 0)
                    return ud.Acc_No;
            }
            catch (Exception e)
            {
                throw e;
            }
            return 0;
        }

        [Route("api/RegisterAPI/Login/{User_id}/{password}")]
        [HttpGet]
        public string Get(string User_id, string password)
        {
            //string result ="";
            try
            {
                var res = db.sp_Login(User_id, password).SingleOrDefault();
                if (res == null)
                {
                    var id = db.UserAccounts.Where(x => x.User_Id == User_id).SingleOrDefault();
                    if (id == null)
                    {
                        return "No Account found with requested UserID";
                    }
                    else
                    {
                        var data = db.UserAccounts.Where(x => x.User_Id == User_id && x.Acc_Status == "Inactive").SingleOrDefault();
                        if (data == null)
                            return "Invalid Credentials";
                        else
                        {
                            //if (data.Acc_Status == "Inactive")
                            return "Your account has been locked. Kindly reset your password for reactivation";
                        }
                    }
                }
                else
                {
                    return "Login Successful";
                }
        }
            catch (Exception e)
            {
                throw e;
            }
            //Console.Write(result);
            //return result;
        }

        [Route("api/RegisterAPI/GenerateRefID/{Acc_No}")]
        [HttpGet]
        public string Get_RefID(int Acc_No)
        {
            try
            {
                var res = db.sp_Generate_RefID(Acc_No);
                if (res > 0)
                {
                    var data = db.User_Details.Where(x => x.Acc_No == Acc_No).SingleOrDefault();
                    if (data == null)
                    {
                        return "Error Occurerd during RefID generation. Contact Admin for more details..";
                    }
                    else
                    {
                        return data.Ref_ID;
                    }
                }
                else
                {
                    return "Error Occurerd during RefID generation. Contact Admin for more details..";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/RegisterAPI/TrackRefID/{RefID}")]
        [HttpGet]
        public string Get( string RefID)
        {
            try
            {
                var res = db.User_Details.Where(x => x.Ref_ID == RefID).SingleOrDefault();
               
                
                    return res.Status;
                
              
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/RegisterAPI/ResetPassByID/{User_ID}/{login_pass}/{trans_pass}")]
        [HttpGet]
        public string Get_ResetPassByID(string User_ID, string login_pass, string trans_pass)
        {
            try
            {
                var res = db.sp_Reset_Pass_ByUserID(User_ID, login_pass, trans_pass);
                if (res > 0)
                    return "Password Reset Successful. Kindly Login again..!";
                else
                    return "Error Occurred while Resetting..! Try Again after some time.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/RegisterAPI/ResetPassByAccNo/{User_ID}/{login_pass}/{trans_pass}")]
        [HttpGet]
        public string Get_ResetPass(int User_ID, string login_pass, string trans_pass)
        {
            try
            {
                var res = db.sp_Reset_Password(User_ID, login_pass, trans_pass);
                if (res > 0)
                    return "Password Reset Successful. Kindly Login again..!";
                else
                    return "Error Occurred while Resetting..! Try Again after some time.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/RegisterAPI/GenerateOTP/{Acc_No}")]
        [HttpGet]
        public string Get_OTP(int Acc_No)
        {
            string receiver_mailid;
          var res = db.User_Details.Where(x => x.Acc_No == Acc_No).SingleOrDefault();
            //var email = db.User_Details.Where(x => x.Acc_No == res. Acc_No).SingleOrDefault();

            if (res == null)
            {
                return "false";
            }
            else
            {
                receiver_mailid = res.Email;
                string from, pass, messageBody, randomCode, Code, to;
                Random rand = new Random();
                randomCode = (rand.Next(999999)).ToString();
                Code = randomCode.ToString();
                MailMessage message = new MailMessage();
                to = receiver_mailid;
                from = "reevafern161@gmail.com";
                pass = "ace123456789";
                messageBody = "Your Reset Code is " + randomCode;
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Body = messageBody;
                message.Subject = "Password Reseting Code";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);
                try
                {
                    smtp.Send(message);
                    return Code;
                    //Literal3.Text = " Code send Successfully !";
                    //Response.Write("");
                }
                catch (Exception ex)
                {
                    throw ex;
                    //Response.Write(ex.Message);
                }
            }
        }


    }
}
