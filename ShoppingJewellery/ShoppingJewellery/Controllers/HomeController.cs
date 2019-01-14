using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingJewellery.Models;
namespace ShoppingJewellery.Controllers
{
    public class HomeController : Controller
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.ViewDisplayItems);
        }
    }
}