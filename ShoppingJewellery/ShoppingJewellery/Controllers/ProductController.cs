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
            if (item == null || img == null || gold == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
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

        [HttpGet]
        public ActionResult Item_required(Item_Require item)
        {
            bool check = dao.CheckItem(item);
            if (check == true)
            {
                return RedirectToAction("ProductList", "ProDuct");
            }
            else
            {
                TempData["msg"] ="<script>alert('Change succesfully');</script>";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}