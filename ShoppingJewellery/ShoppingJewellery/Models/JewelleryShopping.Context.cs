﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JewelleryShopping_dbEntities : DbContext
    {
        public JewelleryShopping_dbEntities()
            : base("name=JewelleryShopping_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DisplayCommonPro> DisplayCommonProes { get; set; }
        public virtual DbSet<AdminLoginMst> AdminLoginMsts { get; set; }
        public virtual DbSet<CatMst> CatMsts { get; set; }
        public virtual DbSet<JewelTypeMst> JewelTypeMsts { get; set; }
        public virtual DbSet<ProdMst> ProdMsts { get; set; }
        public virtual DbSet<ViewDisplayItem> ViewDisplayItems { get; set; }
        public virtual DbSet<ViewFullItem> ViewFullItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<BrandMst> BrandMsts { get; set; }
        public virtual DbSet<CertifyMst> CertifyMsts { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<DimQltyMst> DimQltyMsts { get; set; }
        public virtual DbSet<DimQltySubMst> DimQltySubMsts { get; set; }
        public virtual DbSet<GoldKrtMst> GoldKrtMsts { get; set; }
        public virtual DbSet<Receiver> Receivers { get; set; }
        public virtual DbSet<StoneQltyMst> StoneQltyMsts { get; set; }
        public virtual DbSet<UserRegMst> UserRegMsts { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<DimMst> DimMsts { get; set; }
        public virtual DbSet<GoldMst> GoldMsts { get; set; }
        public virtual DbSet<StoneMst> StoneMsts { get; set; }
        public virtual DbSet<DiamonView> DiamonViews { get; set; }
        public virtual DbSet<GoldView> GoldViews { get; set; }
        public virtual DbSet<SimilarPrice> SimilarPrices { get; set; }
        public virtual DbSet<StoneView> StoneViews { get; set; }
        public virtual DbSet<ProductView> ProductViews { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ItemMst> ItemMsts { get; set; }
        public virtual DbSet<Order_> Order_ { get; set; }
    }
}
