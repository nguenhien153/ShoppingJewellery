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
    
    public partial class StoneMst
    {
        public string Style_Code { get; set; }
        public string StoneQlty_ID { get; set; }
        public decimal Stone_Gm { get; set; }
        public decimal Stone_Pcs { get; set; }
        public decimal Stone_Crt { get; set; }
        public decimal Stone_Rate { get; set; }
        public decimal Stone_Amt { get; set; }
        public int No { get; set; }
        public int ID { get; set; }
    
        public virtual ItemMst ItemMst { get; set; }
        public virtual StoneQltyMst StoneQltyMst { get; set; }
    }
}
