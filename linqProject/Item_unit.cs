namespace linqProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item_unit
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int item_code { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string unit_name { get; set; }

        public int unit_value { get; set; }

        public virtual Item Item { get; set; }
    }
}
