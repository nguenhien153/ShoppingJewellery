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
        public string JewelTypeMst1 { get; set; }
        public string Jewellery_Type { get; set; }
        public string ID_Prod { get; set; }
    
        public virtual ProdMst ProdMst { get; set; }
    }
}