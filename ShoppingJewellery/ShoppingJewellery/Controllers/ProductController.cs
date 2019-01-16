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
            if(item==null)
            {
                return RedirectToAction("Index","Home");
            }else
            {
                return View(item);
            }
           
        }

        public ActionResult ProductList()
        {
            return View(dao.GetListProductToDisplay());
        }
    }
}