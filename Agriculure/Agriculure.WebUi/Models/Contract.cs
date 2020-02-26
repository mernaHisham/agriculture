namespace Agriculure.WebUi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contract
    {
        public long ID { get; set; }

        public long OfferID { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }

        public long? buyerID { get; set; }

        public long? sellerID { get; set; }

        public DateTime? requestDate { get; set; }

        public bool? status { get; set; }

        public DateTime? acceptDate { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
