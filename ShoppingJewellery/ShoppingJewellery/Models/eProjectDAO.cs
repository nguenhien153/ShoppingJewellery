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
            var pro2 = db.Images.Where(item => item.Style_Colde.Equals(Style_Cole)).ToList();
            return pro2;
        }

        public List<ViewDisplayItem> GetListProductToDisplay()
        {
            return db.ViewDisplayItems.ToList();
        }
    }

    public class ViewModelItem_Imgage
    {
        public ViewFullItem ViewFullItems { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}