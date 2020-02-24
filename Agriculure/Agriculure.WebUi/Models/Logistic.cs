namespace Agriculure.WebUi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Logistic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Logistic()
        {
            Retailers = new HashSet<Retailer>();
            Transactions = new HashSet<Transaction>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string LogisticsName { get; set; }

        [StringLength(200)]
        public string LogisticsAddress { get; set; }

        [StringLength(50)]
        public string LogisticsRole { get; set; }

        [StringLength(100)]
        public string LogisticsEmail { get; set; }

        [StringLength(100)]
        public string LogisticsLicense { get; set; }

        [StringLength(100)]
        public string LogisticsNID { get; set; }

        public long ProducerID { get; set; }

        [StringLength(50)]
        public string LogisticPassword { get; set; }

        public virtual Producer Producer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Retailer> Retailers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
