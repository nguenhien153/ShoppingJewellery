using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var gold = dao.GetMetal(Style_Code);
            var stone = dao.GetStone(Style_Code);
            var diamond = dao.GetDiamon(Style_Code);
            if(item==null || img==null || gold==null)
            {
                return RedirectToAction("Index","Home");
            }else
            {
               
                ViewModel image_item = new ViewModel();
                image_item.GoldProduct = gold;
                image_item.StoneProduct = stone;
                image_item.DiamonProduct = diamond;
                image_item.Images = img;
                image_item.ViewFullItems = item;
                return View(image_item);
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