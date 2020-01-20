using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace crystalthoughts.Models
{
    public class Property
    {
        public string Condition1 { get; set; }
        public string Condition2 { get; set; }
        public string Condition3 { get; set; }
        public string Condition4 { get; set; }
        public string Condition5 { get; set; }
        public string Condition6 { get; set; }
        public string Condition7 { get; set; }
        public string Condition8 { get; set; }
        public string onTable { get; set; }
    }
    public class Videolist
    {
        public string Title { get; set; }
        public string Video { get; set; }
        public string Description { get; set; }
        public string Videoimage { get; set; }
        public string Videoid { get; set; }
        public string publishat { get; set; }
    }
    public class Account_Data
    {
        public string Account_id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Paasword { get; set; }
        public string Phoneno { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string LoginDate { get; set; }
        public string Status { get; set; }
        public string comment_id { get; set; }
        public string user_id { get; set; }
        public string comment { get; set; }
        public string Date { get; set; }
        public string videoid { get; set; }
    }

    public class Contectusdata
    {
        public string Contectid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ContectDate { get; set; }

    }

    public class blogdata
    {
        public string blogid { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string videourl { get; set; }
        public string discription { get; set; }
        public string blogdate { get; set; }
        public string status { get; set; }
    }

    public class aboutus
    {
        public string aboutid { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string Description { get; set; }

    }
}