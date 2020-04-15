using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agriculure.WebUi.ViewModel
{
    public class UserVM
    {
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public long RoleID { get; set; }
        public string Liecnse { get; set; }
        [Required]
        public string NID { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "password must contain small and capital letters,numbers,at least one special charcter and minimum 8 charcters..")]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }

    }
}