using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingJewellery.Models
{
    public class eProjectDAO
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();

        public List<DisplayCommonPro> GetProCommon()
        {
            return db.DisplayCommonProes.ToList();
        }

        public ViewFullItem GetDetailsProduct(string Style_Code)
        {
            var pro = db.ViewFullItems.First(item => item.Style_Code.Trim().Equals(Style_Code.Trim()));
            return pro;
        }
        public List<ViewDisplayItem> GetListProductToDisplay()
        {
            return db.ViewDisplayItems.ToList();
        }
        public List<SimilarPrice> GetSimilar()
        {
            return db.SimilarPrices.ToList();
        }

        public List<Image> GetImageProduct(string Style_Cole)
        {
            var pro2 = db.Images.Where(item => item.Style_Colde.Trim().Equals(Style_Cole.Trim())).ToList();
            return pro2;
        }
        public List<GoldView> GetMetal(string Style_Cole)
        {
            var pro = db.GoldViews.Where(item => item.Style_code.Trim().Equals(Style_Cole.Trim()));
            var pro2 = pro.OrderBy(item => item.No).ToList();
            return pro2;
        }
        public List<DiamonView> GetDiamon(string Style_Cole)
        {
            var pro = db.DiamonViews.Where(item => item.Style_Code.Trim().Equals(Style_Cole.Trim()));
            var pro2 = pro.OrderBy(item => item.No).ToList();
            return pro2;
        }
        public List<StoneView> GetStone(string Style_Cole)
        {
            var pro = db.StoneViews.Where(item => item.Style_Code.Trim().Equals(Style_Cole.Trim()));
            var pro2 = pro.OrderBy(item => item.No).ToList();
            return pro2;
        }

        public GoldView GetNoGold(int No, string Style_code)
        {
            var pro = db.GoldViews.First(item => item.No == No && item.Style_code.Trim().Equals(Style_code.Trim()));
            return pro;
        }
        public DiamonView GetNoDiamond(int No, string Style_code)
        {
            var pro = db.DiamonViews.First(item => item.No == No && item.Style_Code.Trim().Equals(Style_code.Trim()));
            return pro;
        }
        public StoneView GetNoStone(int No, string Style_code)
        {
            var pro = db.StoneViews.First(item => item.No == No && item.Style_Code.Trim().Equals(Style_code.Trim()));
            return pro;
        }

       

    }

    public class ViewModel
    {
        public IEnumerable<SimilarPrice> SimilarPrices { get; set; }
        public ViewFullItem ViewFullItems { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<GoldView> GoldProduct { get; set; }
        public IEnumerable<DiamonView> DiamonProduct  { get; set; }
        public IEnumerable<StoneView> StoneProduct { get; set; }
    }

    public class ItemCart
    {
        private ItemMst itemm = new ItemMst();
        private int DiamondNo;
        private int GoldNo;
        private int Size;
        private int StoneNo;
        private decimal UnitPrice;
        private int quantity;
        private string img;
        private string namestone;
        private string namediamond;
        private string namemetal;

        public int GoldNo1 { get => GoldNo; set => GoldNo = value; }
        public int Size1 { get => Size; set => Size = value; }
        public int StoneNo1 { get => StoneNo; set => StoneNo = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public ItemMst Itemm { get => itemm; set => itemm = value; }
        public int DiamondNo1 { get => DiamondNo; set => DiamondNo = value; }
        public decimal UnitPrice1 { get => UnitPrice; set => UnitPrice = value; }
        public string Img { get => img; set => img = value; }
        public string Namestone { get => namestone; set => namestone = value; }
        public string Namediamond { get => namediamond; set => namediamond = value; }
        public string Namemetal { get => namemetal; set => namemetal = value; }

        public ItemCart(ItemMst itemm, int quantity,int diamon,int gold, int stone,int size, decimal unitprice,string img, string namemetal, string namediamond, string namestone)
        {
            this.Itemm = itemm;
            this.DiamondNo1 = diamon;
            this.StoneNo1 = stone;
            this.GoldNo1 = gold;
            this.Size1 = size;
            this.Quantity = quantity;
            this.UnitPrice1 = unitprice;
            this.Img = img;
            this.Namestone = namestone;
            this.Namediamond = namediamond;
            this.Namemetal = namemetal;
        }
    }
   
    public class Numberorther
    {
        public IEnumerable<GoldView> GoldProduct { get; set; }
        public IEnumerable<DiamonView> DiamonProduct { get; set; }
        public IEnumerable<StoneView> StoneProduct { get; set; }
    }

    public class Mail_send
    {
        private decimal total;
        private string fullname;
        private string address;
        private string zip;
        private string phone;
        private string email;
        private string instruction;
        private string date;

        public decimal Total { get => total; set => total = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public string Address { get => address; set => address = value; }
        public string Zip { get => zip; set => zip = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string Instruction { get => instruction; set => instruction = value; }
        public string Date { get => date; set => date = value; }
    }


    public class Order_deta
    {
        private int ID_Order,Gold_No,Diamond_No,Stone_No,Size,Quantity;
        private string Style_code;
        private decimal UnitPrice, Total;

        public int ID_Order1 { get => ID_Order; set => ID_Order = value; }
        public int Gold_No1 { get => Gold_No; set => Gold_No = value; }
        public int Diamond_No1 { get => Diamond_No; set => Diamond_No = value; }
        public int Stone_No1 { get => Stone_No; set => Stone_No = value; }
        public int Size1 { get => Size; set => Size = value; }
        public int Quantity1 { get => Quantity; set => Quantity = value; }
        public string Style_code1 { get => Style_code; set => Style_code = value; }
        public decimal UnitPrice1 { get => UnitPrice; set => UnitPrice = value; }
        public decimal Total1 { get => Total; set => Total = value; }

    }



}