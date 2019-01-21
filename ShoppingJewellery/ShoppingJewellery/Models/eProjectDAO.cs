using System;
using System.Collections.Generic;
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
            var pro = db.ViewFullItems.First(item => item.Style_Code.Equals(Style_Code));
            return pro;
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

        public List<ViewDisplayItem> GetListProductToDisplay()
        {
            return db.ViewDisplayItems.ToList();
        }

        public GoldView GetNoGold(int No,string Style_code)
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
        public ViewFullItem ViewFullItems { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<GoldView> GoldProduct { get; set; }
        public IEnumerable<DiamonView> DiamonProduct  { get; set; }
        public IEnumerable<StoneView> StoneProduct { get; set; }
    }
}