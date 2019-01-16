using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingJewellery.Models;

namespace ShoppingJewellery.Controllers
{
    public class AdminController : Controller
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        Account sv = new Account();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                
                return View(sv.Get(Session["admin"].ToString()));
            }
            return View("Login");
        }

        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var result = db.AdminLoginMsts.Count(i => i.Username.Equals(username) && i.Password.Equals(password));
            if (result != 0)
            {
                var role = sv.Get(username);
                if (role.role == false) {
                    Session["admin"] = username;
                    return RedirectToAction("Index");
                }
                if (role.role == true) {
                    Session["SuperAd"] = username;
                    return RedirectToAction("Index");
                }     
            }
            Session["check"] = "fail";
            return View();
        }

        public ActionResult ChangePass()
        {
            if (Session["admin"] == null)
            {
                return View("Login");
            }
            AdminLoginMst r = sv.Get(Session["admin"].ToString());
            return View(r);
        }
        [HttpPost]
        public ActionResult ChangePass(string OldPass, string NewPass, string CNewPass)
        {
            if (ModelState.IsValid)
            {
                var edAD = sv.Get(Session["admin"].ToString());
                if (!edAD.Password.Equals(OldPass))
                {
                    Session["check"] = "FOld";
                    return View();
                }
                if (!NewPass.Equals(CNewPass))
                {
                    Session["check"] = "FNSame";
                    return View();
                }
                edAD.Password = NewPass;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Password not valid");
            return View();
        }

        public ActionResult ManageUser()
        {
            if (Session["admin"] != null)
            {
                return View(db.UserRegMsts.ToList());
            }
            return View("Login");
        }

        public ActionResult ManageFeedback()
        {
            if (Session["admin"] != null)
            {
                return View(db.UserRegMsts.ToList());
            }
            return View("Login");
        }

        public ActionResult ManageAdmin()
        {
            if (Session["SuperAd"] != null)
            {
                return View(db.AdminLoginMsts.ToList());
            }
            return RedirectToAction("Index");
        }
    }
}