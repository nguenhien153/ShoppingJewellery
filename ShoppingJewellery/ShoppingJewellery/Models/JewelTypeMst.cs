//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoppingJewellery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class JewelTypeMst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JewelTypeMst()
        {
            this.ItemMsts = new HashSet<ItemMst>();
        }
    
        public string JewelTypeMst1 { get; set; }
        public string Jewellery_Type { get; set; }
        public string ID_Prod { get; set; }
    
        public virtual ProdMst ProdMst { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemMst> ItemMsts { get; set; }
    }
}
