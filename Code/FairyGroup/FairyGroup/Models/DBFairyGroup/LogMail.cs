//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FairyGroup.Models.DBFairyGroup
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogMail
    {
        public int SendID { get; set; }
        public string SendFrom { get; set; }
        public string SendTo { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string uEmail { get; set; }
        public string mSubject { get; set; }
        public string mBody { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string LogMSG { get; set; }
        public string SendStatus { get; set; }
    }
}
