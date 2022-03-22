namespace linqProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class item_supplyPermition
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SP_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int item_id { get; set; }

        public int quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime productionDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? expiryTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime expiryDate { get; set; }

        public virtual Item Item { get; set; }

        public virtual supplyPermition supplyPermition { get; set; }
    }
}
