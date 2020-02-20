using Agriculure.WebUi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agriculure.WebUi.ViewModel
{
    public class FarmerVM
    {
        public long ID { get; set; }

        [Required(ErrorMessage ="Enter this Field")]
        [StringLength(100)]
        public string FarmerName { get; set; }

        [StringLength(200)]
        public string FarmerAddress { get; set; }

        [Required(ErrorMessage = "Enter this Field")]
        [StringLength(100)]
        public string FarmerEmail { get; set; }

        [StringLength(50)]
        public string FarmerRole { get; set; }

        [StringLength(100)]
        public string FarmerLiecnse { get; set; }

        [StringLength(100)]
        public string FarmerNID { get; set; }

        public long FarmerSID { get; set; }
    }
}