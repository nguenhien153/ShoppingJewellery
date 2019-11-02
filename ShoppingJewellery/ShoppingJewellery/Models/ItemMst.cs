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
    
    public partial class ItemMst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemMst()
        {
            this.DimMsts = new HashSet<DimMst>();
            this.GoldMsts = new HashSet<GoldMst>();
            this.Images = new HashSet<Image>();
            this.Order_Details = new HashSet<Order_Details>();
            this.StoneMsts = new HashSet<StoneMst>();
        }
    
        public string Style_Code { get; set; }
        public decimal Pairs { get; set; }
        public string Brand_ID { get; set; }
        public decimal Quantity { get; set; }
        public string Cat_ID { get; set; }
        public string Prod_Quality { get; set; }
        public string Certify_ID { get; set; }
        public string Prod_ID { get; set; }
        public string JewelleryType_ID { get; set; }
        public decimal Net_Gold { get; set; }
        public decimal Tot_Gross_Wt { get; set; }
        public decimal Stone_Making { get; set; }
        public decimal Other_Making { get; set; }
        public decimal Tot_Making { get; set; }
        public decimal MRP { get; set; }
        public int size { get; set; }
        public decimal Gold_Making { get; set; }
        public string Img { get; set; }
        public decimal Stone_Wt { get; set; }
    
        public virtual BrandMst BrandMst { get; set; }
        public virtual CatMst CatMst { get; set; }
        public virtual CertifyMst CertifyMst { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DimMst> DimMsts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldMst> GoldMsts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }
        public virtual JewelTypeMst JewelTypeMst { get; set; }
        public virtual ProdMst ProdMst { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoneMst> StoneMsts { get; set; }
    }
}