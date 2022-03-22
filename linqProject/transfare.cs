namespace linqProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class transfare
    {
        public int from_store { get; set; }

        public int to_store { get; set; }

        public int item_code { get; set; }

        public int quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime productionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime expiryDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? expiryTime { get; set; }

        public int supplier_id { get; set; }

        public int id { get; set; }

        public virtual Item Item { get; set; }

        public virtual Store Store { get; set; }

        public virtual Store Store1 { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
