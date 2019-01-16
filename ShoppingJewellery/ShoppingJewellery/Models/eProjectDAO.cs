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
            var pro = db.ViewFullItems.First(item => item.Style_Code.Contains(Style_Code));
            return pro;
        }

        public List<ViewDisplayItem> GetListProductToDisplay()
        {
            return db.ViewDisplayItems.ToList();
        }
    }
}