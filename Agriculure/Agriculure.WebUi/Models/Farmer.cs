namespace Agriculure.WebUi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Farmer")]
    public partial class Farmer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Farmer()
        {
            Producers = new HashSet<Producer>();
            Transactions = new HashSet<Transaction>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FarmerName { get; set; }

        [StringLength(200)]
        public string FarmerAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string FarmerEmail { get; set; }

        [StringLength(50)]
        public string FarmerRole { get; set; }

        [StringLength(100)]
        public string FarmerLiecnse { get; set; }

        [StringLength(100)]
        public string FarmerNID { get; set; }

        public long FarmerSID { get; set; }

        [StringLength(50)]
        public string FarmerPassword { get; set; }

        public virtual FarmSupplier FarmSupplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producer> Producers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
