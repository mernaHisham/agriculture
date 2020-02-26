using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agriculure.WebUi.Custom_Classes
{
    public class RegisterationObject
    {
        [Required(ErrorMessage ="Required")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public int ID { get; set; }//role id
        public string Address { get; set; }
        public string Lisence { get; set; }
        public long PhoneNumber { get; set; }
        [Required]
        public long NationalId { get; set; }
        [Required]
        public string Email { get; set; }
    }
}