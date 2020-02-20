namespace Agriculure.WebUi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FarmSupplier")]
    public partial class FarmSupplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FarmSupplier()
        {
            Farmers = new HashSet<Farmer>();
            Transactions = new HashSet<Transaction>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FarmerSName { get; set; }

        [StringLength(200)]
        public string FarmerSAddress { get; set; }

        [StringLength(50)]
        public string FarmerSRole { get; set; }

        [StringLength(100)]
        public string FarmerSEmail { get; set; }

        [StringLength(50)]
        public string FarmerSLicense { get; set; }

        [StringLength(50)]
        public string FarmerSNID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Farmer> Farmers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
