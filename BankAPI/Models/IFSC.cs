//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class IFSC
    {
        public IFSC()
        {
            this.User_Details = new HashSet<User_Details>();
        }
    
        public string Branch_Name { get; set; }
        public string IFSC_code { get; set; }
    
        public virtual ICollection<User_Details> User_Details { get; set; }
    }
}
