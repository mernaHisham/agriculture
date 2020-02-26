using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agriculure.WebUi.Custom_Classes
{
    public class RegisterationObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public int StackHolderTypeId { get; set; }
        public string Address { get; set; }
        public string Lisence { get; set; }
        public long PhoneNumber { get; set; }
        public long NationalId { get; set; }
        public string Email { get; set; }
    }
}