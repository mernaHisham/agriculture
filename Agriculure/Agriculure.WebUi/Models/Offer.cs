namespace Agriculure.WebUi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Offer")]
    public partial class Offer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Offer()
        {
            Contracts = new HashSet<Contract>();
        }

        public long ID { get; set; }

        public long ProductID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Status { get; set; }

        public long offerowner { get; set; }

        [Required]
        [StringLength(4000)]
        public string Descreption { get; set; }

        [Required]
        [StringLength(50)]
        public string unit { get; set; }

        public decimal quntity { get; set; }

        public decimal price { get; set; }

        [Required]
        [StringLength(50)]
        public string currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
