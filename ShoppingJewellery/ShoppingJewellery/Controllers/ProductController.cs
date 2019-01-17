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
        eProjectDAO dao = new eProjectDAO();
        public ActionResult Product(string Style_Code)
        {
            var item = dao.GetDetailsProduct(Style_Code);
            var img = dao.GetImageProduct(Style_Code);
            if(item==null || img==null)
            {
                return RedirectToAction("Index","Home");
            }else
            {
                ViewModelItem_Imgage Item_Img = new ViewModelItem_Imgage();
                Item_Img.Images = img;
                Item_Img.ViewFullItems = item;
                return View(item);
            }
           
        }

        public ActionResult ProductList()
        {
            return View(dao.GetListProductToDisplay());
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}