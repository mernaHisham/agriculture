namespace Agriculure.WebUi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Producer")]
    public partial class Producer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producer()
        {
            Logistics = new HashSet<Logistic>();
            Retailers = new HashSet<Retailer>();
            Transactions = new HashSet<Transaction>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProducerName { get; set; }

        [StringLength(200)]
        public string ProducerAddress { get; set; }

        [StringLength(50)]
        public string ProducerRole { get; set; }

        [StringLength(100)]
        public string ProducerEmail { get; set; }

        [StringLength(100)]
        public string ProducerLicense { get; set; }

        [StringLength(100)]
        public string ProducerNID { get; set; }

        public long FarmerID { get; set; }

        [StringLength(50)]
        public string ProducerPassword { get; set; }

        public virtual Farmer Farmer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Logistic> Logistics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Retailer> Retailers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
