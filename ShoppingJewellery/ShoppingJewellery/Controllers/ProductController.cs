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
            if (item == null || img == null || gold == null || stone == null || diamond == null)
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
                image_item.SimilarPrices = dao.GetSimilar();
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

        public ActionResult GetNoMetal(int No, string Style_code)
        {
            var pro = dao.GetNoGold(No, Style_code);
            return PartialView(pro);
        }
        public ActionResult GetNoDiamond(int No, string Style_code)
        {
            var pro = dao.GetNoDiamond(No, Style_code);
            return PartialView(pro);
        }
        public ActionResult GetNoStone(int No, string Style_code)
        {
            var pro = dao.GetNoStone(No, Style_code);
            return PartialView(pro);
        }

        //public ActionResult CartShhoppng(string Style_cod, int siz, int NGold, int NoDiamon, int NoGe, decimal total_bran)
        //{
        //   bool checkk= dao.CartShopping( Style_cod,  siz, NGold, NoDiamon,  NoGe, total_bran);
        //    if(checkk==true)
        //    {
        //        return RedirectToAction("ProductList", "Product");
        //    }
        //    else
        //    {
        //        TempData["msg"] = "<script>alert('Add Failse');</script>";
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

       
    }
}