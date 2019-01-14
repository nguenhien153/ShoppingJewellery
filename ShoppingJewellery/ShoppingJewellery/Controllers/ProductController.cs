using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingJewellery.Models;
namespace ShoppingJewellery.Controllers
{
    public class ProductController : Controller
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        // GET: Product
        public ActionResult Product(string Style_Code)
        {
            var item = db.ViewFullItems.First(itm=>itm.Style_Code.Contains(Style_Code));
            if(item==null)
            {
                return RedirectToAction("Index", "Home");
            }else
            {
                return View(item);
            }
           
        }
    }
}