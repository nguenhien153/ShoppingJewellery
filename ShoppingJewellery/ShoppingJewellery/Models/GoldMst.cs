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
    
    public partial class GoldMst
    {
        public string Style_code { get; set; }
        public string GoldType_ID { get; set; }
        public decimal Gold_Wt { get; set; }
        public decimal Wstg_Per { get; set; }
        public decimal Total_weight { get; set; }
        public decimal Wstq { get; set; }
        public int No { get; set; }
        public int ID { get; set; }
    
        public virtual GoldKrtMst GoldKrtMst { get; set; }
        public virtual ItemMst ItemMst { get; set; }
    }
}
