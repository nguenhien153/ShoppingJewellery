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
        eProjectDAO dao = new eProjectDAO();
        // GET: Home
        public ActionResult Index()
        {
            return View(dao.GetProCommon());
        }
    }
}