namespace linqProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("supplyPermition")]
    public partial class supplyPermition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public supplyPermition()
        {
            item_supplyPermition = new HashSet<item_supplyPermition>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public int supplier_id { get; set; }

        public int store_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item_supplyPermition> item_supplyPermition { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
