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
        Control service = new Control();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                
                return View(service.GetAd(Session["admin"].ToString()));
            }
            return View("Login");
        }

        public ActionResult Login()
        {
            Session["admin"] = null;
            Session["SuperAd"] = null;
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var result = db.AdminLoginMsts.Count(i => i.Username.Equals(username) && i.Password.Equals(password));
            //if (result != 0)
            //{
            //    var role = service.GetAd(username);
            //    if (role.role.Equals(0)) {
                    Session["admin"] = username;
                    return RedirectToAction("Index");
            //    }
            //    if (role.role.Equals(1)) {
            //        Session["SuperAd"] = username;
            //        return RedirectToAction("Index");
            //    }     
            //}
            //Session["check"] = "fail";
           // return View();
        }

        public ActionResult ChangePass()
        {
            if (Session["admin"] == null)
            {
                return View("Login");
            }
            AdminLoginMst r = service.GetAd(Session["admin"].ToString());
            return View(r);
        }
        [HttpPost]
        public ActionResult ChangePass(string OldPass, string NewPass, string CNewPass)
        {
            if (ModelState.IsValid)
            {
                string username = Session["admin"].ToString();
                AdminLoginMst edAD = db.AdminLoginMsts.Single(i => i.Username.Equals(username));
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

        public ActionResult ManageAdmin()
        {
            if (Session["admin"] != null)
            {
                return View(db.AdminLoginMsts.ToList());
            }
            return RedirectToAction("Index");
        }

        public ActionResult ManageFeedBack()
        {
            if (Session["admin"] != null)
            {
                return View(db.Feedbacks.ToList());
            }
            return View("Login");
        }

        public ActionResult ReplyFeedBack(int id)
        {
            if (Session["admin"] != null)
            {
                return View(service.GetByID(id));
            }
            return View("Login");
        }
        [HttpPost]
        public ActionResult ReplyFeedBack(Feedback Fb, string Redate)
        {
            if (Session["admin"] != null)
            {
                Feedback edFb = db.Feedbacks.Single(i => i.FId == Fb.FId);
                edFb.ReDate = Redate;
                edFb.ReComment = Fb.ReComment;
                if (ModelState.IsValid) {                
                    db.SaveChanges();
                    return RedirectToAction("ManageFeedback");
                }
                ModelState.AddModelError("", "Invalid");
                return View();
            }
            return View("Login");
        }

        public ActionResult DetailsFeedBack(int id)
        {
            if (Session["admin"] != null)
            {
                return View(service.GetByID(id));
            }
            return View("Login");
        }
    }
}