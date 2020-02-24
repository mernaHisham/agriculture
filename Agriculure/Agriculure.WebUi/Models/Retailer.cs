namespace Agriculure.WebUi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Retailer")]
    public partial class Retailer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Retailer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string RetailerName { get; set; }

        [StringLength(200)]
        public string RetailerAddress { get; set; }

        [StringLength(50)]
        public string RetailerRole { get; set; }

        [StringLength(100)]
        public string RetailerEmail { get; set; }

        [StringLength(100)]
        public string RetailerLicense { get; set; }

        [StringLength(100)]
        public string RetailerNID { get; set; }

        public long ProducerID { get; set; }

        public long LogisticsID { get; set; }

        [Required]
        [StringLength(500)]
        public string RetailerPassword { get; set; }

        public virtual Logistic Logistic { get; set; }

        public virtual Producer Producer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
