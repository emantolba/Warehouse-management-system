namespace linqProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exchangePermition")]
    public partial class exchangePermition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public exchangePermition()
        {
            item_exchamgePermition = new HashSet<item_exchamgePermition>();
        }

        public int id { get; set; }

        public DateTime date { get; set; }

        public int client_id { get; set; }

        public int store_id { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Store Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item_exchamgePermition> item_exchamgePermition { get; set; }
    }
}
