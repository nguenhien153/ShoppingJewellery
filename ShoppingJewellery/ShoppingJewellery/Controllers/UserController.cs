﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingJewellery.Models;
namespace ShoppingJewellery.Controllers
{
    public class UserController : Controller
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session["user"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var result = db.UserRegMsts.Count(i => i.userID.Equals(username) && i.password.Equals(password));
            if (result != 0)
            {
                Session["user"] = username;
                return RedirectToAction("Index","Home");
            }
            Session["check"] = "fail";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserRegMst User)
        {
            if (ModelState.IsValid)
            {
                db.UserRegMsts.Add(User);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError("", "Invalid");
            return View();
        }

        public ActionResult UpdateUser()
        {
            var r = db.UserRegMsts.Single(i => i.userID.Equals(Session["user"]));
            return View(r);
        }
        [HttpPost]
        public ActionResult UpdateUser(UserRegMst User)
        {
            if (ModelState.IsValid)
            {
                var edUser = db.UserRegMsts.Single(i => i.userID.Equals(Session["user"]));
                edUser.mobNo = User.mobNo;
                edUser.emailID = User.emailID;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            Session["check"] = "fail";
            ModelState.AddModelError("", "Invalid");
            return View();
        }

        public ActionResult ChangePass()
        {
            var r = db.UserRegMsts.Single(i => i.userID.Equals(Session["user"]));
            return View(r);
        }
        [HttpPost]
        public ActionResult ChangePass(string oldpass, string NewPass, string cNewPass)
        {

            if (ModelState.IsValid)
            {
                var edUser = db.UserRegMsts.Single(i => i.userID.Equals(Session["user"]));
                if (oldpass.Equals(edUser.password) && NewPass.Equals(cNewPass))
                {
                    edUser.password = NewPass;
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
            }
            Session["check"] = "New password not same";
            ModelState.AddModelError("", "Invalid");
            return View();
        }

        public ActionResult Details()
        {
            var r = db.UserRegMsts.Single(i => i.userID.Equals(Session["user"]));
            return View(r);
        }
    }
}