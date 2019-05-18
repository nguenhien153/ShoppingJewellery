using ShoppingJewellery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ShoppingJewellery.Controllers
{
    public class AdminController : Controller
    {
        Adminn dao = new Adminn();
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        // GET: Admin

        #region Common
        public ActionResult Login()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                string username = Request.Cookies["username"].Value.ToString();
                string password = Request.Cookies["password"].Value.ToString();

                string check = dao.Verify_Login(password, username);
                if (!check.Equals("false"))
                {
                    if (check.Equals("Super_Admin"))
                    {
                        Session["admin"] = "SuperAdmin";
                    }
                    if (check.Equals("Admin"))
                    {
                        Session["admin"] = "Admin";
                    }

                    Session["username"] = username;
                    return RedirectToAction("HomeAdmin");
                }
                else
                {
                    return View();
                }
            }
            else if (Session["admin"] != null)
            {
                return RedirectToAction("HomeAdmin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(string username, string password, bool? remember)
        {

            string check = dao.Verify_Login(password, username);
            if (!check.Equals("false"))
            {
                if (check.Equals("Super_Admin"))
                {
                    Session["admin"] = "SuperAdmin";
                }
                if (check.Equals("Admin"))
                {
                    Session["admin"] = "Admin";
                }
                if (remember == true)
                {
                    HttpCookie usernamee = new HttpCookie("username");
                    usernamee.Expires = DateTime.Now.AddDays(30);
                    usernamee.Value = username;
                    Response.SetCookie(usernamee);

                    HttpCookie passwordd = new HttpCookie("password");
                    passwordd.Expires = DateTime.Now.AddDays(30);
                    passwordd.Value = password;
                    Response.SetCookie(passwordd);
                }
                Session["username"] = username;
                return RedirectToAction("HomeAdmin");
            }
            else
            {
                ModelState.AddModelError("", "Login False !!! Username or Password invalid.");
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpCookie usernamee = new HttpCookie("username");
            usernamee.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(usernamee);

            HttpCookie passwordd = new HttpCookie("password");
            passwordd.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(passwordd);

            Session.Remove("admin");
            Session.Remove("username");

            return RedirectToAction("Login");
        }

        public bool CheckLogin()
        {
            if (Session["admin"] != null && Session["username"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult HomeAdmin()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public bool verifyAdmin()
        {
            if (Session["admin"].ToString().Equals("SuperAdmin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Common

        #region AdminAccount
        public ActionResult AdminAccount()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            return View(dao.GetAdminLogins());
        }

        public ActionResult Change_Password(string username)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            var admin = dao.GetAdmin(username);
            if (admin != null)
            {
                return View(admin);
            }
            else
            {
                return RedirectToAction("AdminAccount");
            }
        }

        [HttpPost]
        public ActionResult Change_Password(AdminLoginMst admin)
        {
            if (admin.Username == null || admin.Password == null)
            {
                return RedirectToAction("Change_Password");
            }
            if (dao.ChangePasswordAdmin(admin) == true)
            {
                return RedirectToAction("AdminAccount");
            }
            else
            {
                ModelState.AddModelError("", "Change False" + admin.Username + " " + admin.Password + "" + admin.role.ToString());
                return View();
            }
        }

        public ActionResult Delete_AdminAccount(string username)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            if (!verifyAdmin())
            {
                return RedirectToAction("AdminAccount");
            }
            if (dao.GetUsernameSuperAdmin().Equals("null"))
            {
                return RedirectToAction("AdminAccount");
            }
            if (!CheckLogin() || username.Equals(dao.GetUsernameSuperAdmin()))
            {
                return RedirectToAction("AdminAccount");
            }

            if (dao.DeleteAdminAccount(username))
            {
                return RedirectToAction("AdminAccount");
            }
            else
            {
                ModelState.AddModelError("", "Delete False");
                return View();
            }
        }

        public ActionResult Create_New_AdminAccount()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create_New_AdminAccount(AdminLoginMst admin)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            if (!verifyAdmin())
            {
                return RedirectToAction("AdminAccount");
            }

            int check = dao.CreateAdminAccount(admin);
            if (check == 0)
            {
                Session["create"] = "Create Success";
                return RedirectToAction("Create_New_AdminAccount");
            }
            else if (check == 1)
            {
                Session["create"] = "Username Has Already Existed !!!";
                return RedirectToAction("Create_New_AdminAccount");
            }
            else
            {
                Session["create"] = "Create False";
                return RedirectToAction("Create_New_AdminAccount");
            }
        }
        #endregion AdminAccount 

        #region UserAccount
        public ActionResult UserAccount()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            return View(dao.GetUser());
        }

        public ActionResult UserAccount_Edit(string username)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                return RedirectToAction("UserAccount");
            }
            UserRegMst user = dao.GetSingleUser(username);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("UserAccount");
            }
        }
        [HttpPost]
        public ActionResult UserAccount_Edit(UserRegMst user)
        {
            int check = dao.EditUserAccount(user);
            if (check == 0)
            {
                return RedirectToAction("UserAccount");
            }
            else if (check == 1)
            {
                Session["UserAccount_Edit"] = "false";
                return View();
            }
            else
            {
                Session["UserAccount_Edit"] = "error";
                return View();
            }
        }

        public ActionResult UserAccount_Detele(string username)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            if (!verifyAdmin())
            {
                return RedirectToAction("UserAccount");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                return RedirectToAction("UserAccount");
            }
            int check = dao.DeleteUserAccount(username);
            if (check == 0)
            {
                return RedirectToAction("UserAccount");
            }
            else if (check == 1)
            {
                Session["UserAccount_Detele"] = "false";
                return View();
            }
            else
            {
                Session["UserAccount_Detele"] = "error";
                return View();
            }

        }

        public ActionResult UserAccount_Create()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UserAccount_Create(UserRegMst user)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            if (!verifyAdmin())
            {
                return RedirectToAction("UserAccount");
            }

            int check = dao.CreateUserAccount(user);
            if (check == 0)
            {
                Session["UserAccount_Create"] = "success";
                return RedirectToAction("UserAccount_Create");
            }
            else if (check == 1)
            {
                Session["UserAccount_Create"] = "false";
                return RedirectToAction("UserAccount_Create");
            }
            else
            {
                Session["UserAccount_Create"] = "error";
                return RedirectToAction("UserAccount_Create");
            }

        }
        #endregion UserAccount

        #region UserAddress
        public ActionResult UserAddress_Create(string username)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                return RedirectToAction("UserAccount");
            }
            return View();
        }
        [HttpPost]
        public ActionResult UserAddress_Create(Address add)
        {
            if (!verifyAdmin())
            {
                return RedirectToAction("UserAccount");
            }

            int check = dao.CreateUserAddress(add);
            if (check == 0)
            {
                Session["UserAddress_Create"] = "success";
                return RedirectToAction("UserAddress_Create", "Admin", new { username = add.UserID });
            }
            else if (check == 1)
            {
                Session["UserAddress_Create"] = "false";
                return RedirectToAction("UserAddress_Create", "Admin", new { username = add.UserID });

            }
            else
            {
                Session["UserAddress_Create"] = "error";
                return RedirectToAction("UserAddress_Create", "Admin", new { username = add.UserID });

            }
        }

        public ActionResult UserAddress_Edit(int key)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            var addr = db.Addresses.Find(key);
            if (addr != null)
            {
                return View(addr);
            }
            else
            {
                return RedirectToAction("UserAccount");
            }
        }

        [HttpPost]
        public ActionResult UserAddress_Edit(Address add)
        {
            int check = dao.EditUserAddress(add);
            if (check == 0)
            {
                return RedirectToAction("UserAccount");
            }
            else if (check == 1)
            {
                Session["UserAddress_Edit"] = "false";
                return RedirectToAction("UserAddress_Edit", new { key = add.NorR });
            }
            else
            {
                Session["UserAddress_Edit"] = "error";
                return RedirectToAction("UserAddress_Edit", new { key = add.NorR });
            }
        }

        public ActionResult UserAddress_Delete(int key)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            if (!verifyAdmin())
            {
                return RedirectToAction("UserAccount");
            }
            int check = dao.DeleteUserAddress(key);
            if (check == 0)
            {
                return RedirectToAction("UserAccount");
            }
            else if (check == 1)
            {
                Session["UserAddress_Delete"] = "false";
                return View();
            }
            else
            {
                Session["UserAddress_Delete"] = "error";
                return View();
            }

        }
        #endregion UserAddress

        #region Product
        public ActionResult Product()
        {
            return View(dao.GetProduct());
        }

        public ActionResult Product_Details(string style_code)
        {
            if (string.IsNullOrWhiteSpace(style_code))
            {
                return RedirectToAction("Product");
            }
            var item = dao.GetItem_Pro(style_code);
            var protype = dao.GetProductType(item.Prod_ID);
            List<GoldView> gold_option = dao.GetOptionGold(style_code);
            List<StoneView> stone_option = dao.GetOptionStone(style_code);
            List<DimMst> diamond_option = dao.GetOptionDiamond(style_code);
            if (item != null && protype != null)
            {
                Details_Product jewel = new Details_Product();
                if (stone_option != null && stone_option.Any())
                {
                    jewel.stone_option = stone_option;
                }
                if (diamond_option != null && diamond_option.Any())
                {
                    jewel.diamond_option = diamond_option;
                }
                if (gold_option != null && gold_option.Any())
                {
                    jewel.gold_option = gold_option;
                }
                jewel.protypee = protype;
                jewel.pro_item = item;
                return View(jewel);
            }
            else
            {
                return RedirectToAction("Product");
            }
        }

        [HttpPost]
        public ActionResult Product_Details(ItemMst item, HttpPostedFileBase imagefile, string Protype)
        {
            try
            {
                int check;
                if (imagefile != null)
                {
                    string filename = Path.GetFileName(imagefile.FileName);
                    if (imagefile.ContentLength < 10485760)
                    {
                        bool exists = Directory.Exists(Server.MapPath("/Content/images/Product_Image/" + Protype + "/Img"));
                        if (!exists)
                        {
                            Directory.CreateDirectory(Server.MapPath("/Content/images/Product_Image/" + Protype + "/Img"));

                        }
                        imagefile.SaveAs(Server.MapPath("/Content/images/Product_Image/" + Protype + "/Img/" + filename));
                        check = dao.EditProdductInfor(item, filename);
                    }
                    else
                    {
                        Session["Product_Details"] = "filefalse";
                        return RedirectToAction("Product_Details", "Admin", new { style_code = item.Style_Code.Replace(" ", string.Empty) });
                    }
                }
                else
                {
                    check = dao.EditProdductInfor(item);

                }

                if (check == 0)
                {
                    Session["Product_Details"] = "success";
                }
                else
                {
                    Session["Product_Details"] = "false";

                }
                return RedirectToAction("Product_Details", "Admin", new { style_code = item.Style_Code.Replace(" ", string.Empty) });

            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName.Equals("cat"))
                {
                    Session["Product_Details"] = "catfalse";
                }
                else if (e.ParamName.Equals("brand"))
                {
                    Session["Product_Details"] = "brandfalse";
                }
                else if (e.ParamName.Equals("pro"))
                {
                    Session["Product_Details"] = "profalse";
                }
                else if (e.ParamName.Equals("jew"))
                {
                    Session["Product_Details"] = "jewfalse";
                }
                else if (e.ParamName.Equals("cer"))
                {
                    Session["Product_Details"] = "cerfalse";
                }
                return RedirectToAction("Product_Details", "Admin", new { style_code = item.Style_Code.Replace(" ", string.Empty) });
            }
            catch (Exception)
            {

                Session["Product_Details"] = "error";
                return RedirectToAction("Product_Details", "Admin", new { style_code = item.Style_Code.Replace(" ", string.Empty) });
            }
        }

        public ActionResult Create_Product_Details()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create_Product_Details(ItemMst item, HttpPostedFileBase imagefile)
        {
            try
            {
                string filename = Path.GetFileName(imagefile.FileName);
                if (imagefile.ContentLength < 10485760)
                {
                    int check = dao.ProductInforCreate(item, filename);
                    string Protype = dao.GetProtype(item.Prod_ID);

                    if (check == 0)
                    {
                        Session["Create_Product_Details"] = "success";
                        bool exists = Directory.Exists(Server.MapPath("/Content/images/Product_Image/" + Protype + "/Img"));
                        if (!exists)
                        {
                            Directory.CreateDirectory(Server.MapPath("/Content/images/Product_Image/" + Protype + "/Img"));

                        }
                        imagefile.SaveAs(Server.MapPath("/Content/images/Product_Image/" + Protype + "/Img/" + filename));
                    }
                    else
                    {
                        Session["Create_Product_Details"] = "false";
                    }
                }
                else
                {
                    Session["Create_Product_Details"] = "falseFile";
                }
                return RedirectToAction("Create_Product_Details", "Admin");
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName.Equals("brandFalse"))
                {
                    Session["Create_Product_Details"] = "brandFalse";
                }
                else if (e.ParamName.Equals("catFalse"))
                {
                    Session["Create_Product_Details"] = "catFalse";
                }
                else if (e.ParamName.Equals("proFalse"))
                {
                    Session["Create_Product_Details"] = "proFalse";
                }
                else if (e.ParamName.Equals("cerFalse"))
                {
                    Session["Create_Product_Details"] = "cerFalse";
                }
                else
                {
                    Session["Create_Product_Details"] = "jewFalse";
                }
                return RedirectToAction("Create_Product_Details", "Admin");
            }
            catch (ArgumentException)
            {
                Session["Create_Product_Details"] = "falseFile";
                return RedirectToAction("Create_Product_Details", "Admin");
            }
            catch (Exception)
            {
                Session["Create_Product_Details"] = "systemfalse";
                return RedirectToAction("Create_Product_Details", "Admin");
            }
        }

        public JsonResult Delete_ProductInfor(string stylecode)
        {
            try
            {
                using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
                {
                    var item = dbb.ItemMsts.Find(stylecode);
                    string Protype = dao.GetProtype(item.Prod_ID);
                    if (item != null)
                    {
                        string rootFolder = "/Content/images/Product_Image/" + Protype + "/Img/" + item.Img;
                        bool exists = System.IO.File.Exists(Server.MapPath(rootFolder));
                        if (exists)
                        {
                           System.IO.File.Delete(Server.MapPath(rootFolder));
                        }
                        dbb.ItemMsts.Remove(item);
                        dbb.SaveChanges();
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("false", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (IOException)
            {
                return Json("filefalse");
            }
            catch (Exception)
            {
                return Json("systemfalse", JsonRequestBehavior.AllowGet);
            }
        }
        #region Metal

        public ActionResult MetalOption(int id_item)
        {
            List<string> metal = new List<string>();
            var item = db.GoldMsts.Find(id_item);
            metal.Add(item.ID.ToString());
            metal.Add(item.Style_code);
            metal.Add(item.GoldType_ID);
            metal.Add(item.Gold_Wt.ToString());
            metal.Add(item.No.ToString());
            metal.Add(item.Total_weight.ToString());
            metal.Add(item.Wstg_Per.ToString());
            metal.Add(item.Wstq.ToString());
            return Json(metal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditMetalOption(GoldMst gold)
        {
            try
            {
                int check = dao.MetalOptionEdit(gold);
                if (check == 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("false");
                }
            }
            catch (DbUpdateException)
            {
                return Json("Nofalse");
            }
            catch (ArgumentNullException)
            {
                return Json("metalfalse");
            }
            catch (Exception)
            {
                return Json("systemfalse");
            }
        }


        [HttpPost]
        public ActionResult CreateMetalOption(GoldMst gold)
        {
            try
            {
                int check = dao.MetalOptionCreate(gold);
                if (check == 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("false");
                }
            }
            catch (DbUpdateException)
            {
                return Json("Nofalse");
            }
            catch (ArgumentException)
            {
                return Json("metalfalse");
            }
            catch (Exception)
            {
                return Json("Systemerror");
            }
        }

        public JsonResult DeleteMetalOption(int id_option)
        {
            try
            {
                int check = dao.MetalOptionDetele(id_option);
                if (check == 0)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("systemerror", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Stone

        public JsonResult StoneOption(int id_item)
        {
            List<string> stone = new List<string>();
            var item = dao.GetStone(id_item);
            stone.Add(item.StoneQlty_ID.Replace(" ", string.Empty));
            stone.Add(item.Stone_Gm.ToString());
            stone.Add(item.Stone_Pcs.ToString());
            stone.Add(item.Stone_Crt.ToString());
            stone.Add(item.Stone_Rate.ToString());
            stone.Add(item.Stone_Amt.ToString());
            stone.Add(item.No.ToString());

            return Json(stone, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditStoneOption(StoneMst stone)
        {
            try
            {
                int check = dao.EditOptionStone(stone);
                if (check == 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("false");
                }
            }
            catch (DbUpdateException)
            {
                return Json("Nofalse");
            }
            catch (ArgumentNullException)
            {
                return Json("metalfalse");
            }
            catch (Exception)
            {
                return Json("systemfalse");
            }
        }

        [HttpPost]
        public JsonResult CreateStoneOption(StoneMst stone)
        {
            try
            {
                int check = dao.CreateOptionStone(stone);
                if (check == 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("false");
                }
            }
            catch (DbUpdateException)
            {
                return Json("Nofalse");
            }
            catch (ArgumentException)
            {
                return Json("stonefalse");
            }
            catch (Exception)
            {
                return Json("Systemerror");
            }
        }

        public JsonResult DeleteStoneOption(int id)
        {
            try
            {
                int check = dao.DeleteOptionStone(id);
                if (check == 0)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("systemerror", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Stone

        #region Diamond

        [HttpGet]
        public JsonResult DiamondOption(int id_item)
        {
            List<string> diamond = new List<string>();
            var item = dao.GetDiamond(id_item);

            diamond.Add(item.No.ToString());

            diamond.Add(item.DimQlty_ID);
            diamond.Add(item.Dim_Crt.ToString());
            diamond.Add(item.Dim_Pcs.ToString());
            diamond.Add(item.Dim_Gm.ToString());
            diamond.Add(item.Dim_Size.ToString());
            diamond.Add(item.Dim_Rate.ToString());
            diamond.Add(item.Dim_Amt.ToString());

            diamond.Add(item.DimSubType_ID);
            diamond.Add(item.Dim_CrtSub.ToString());
            diamond.Add(item.Dim_PcsSub.ToString());
            diamond.Add(item.Dim_GmSub.ToString());
            diamond.Add(item.Dim_SizeSub.ToString());
            diamond.Add(item.Dim_RateSub.ToString());
            diamond.Add(item.Dim_AmtSub.ToString());

            diamond.Add(item.Total_AmtAll.ToString());

            return Json(diamond, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditDiamondOption(DimMst diamond)
        {
            try
            {
                int check = dao.EditOptionDiamond(diamond);
                if (check == 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("false");
                }
            }
            catch (DbUpdateException)
            {
                return Json("Nofalse");
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName.Equals("mainFalse"))
                {
                    return Json("mainFalse");
                }
                else
                {
                    return Json("subFalse");
                }
            }
            catch (Exception)
            {
                return Json("systemfalse");
            }
        }

        [HttpPost]
        public JsonResult CreateDiamondOption(DimMst diamond)
        {
            try
            {
                int check = dao.CreateOptionDiamond(diamond);
                if (check == 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("false");
                }
            }
            catch (DbUpdateException)
            {
                return Json("Nofalse");
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName.Equals("mainFalse"))
                {
                    return Json("mainFalse");
                }
                else
                {
                    return Json("subFalse");
                }
            }
            catch (Exception)
            {
                return Json("systemfalse");
            }
        }

        [HttpGet]
        public JsonResult DeleteDiamondOption(int id_item)
        {
            try
            {
                int check = dao.DeleteOptionDiamond(id_item);
                if (check == 0)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("systemerror", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Diamond

        #region Autocomplete
        public ActionResult GetStyle_code(string term)
        {
            List<string> style_code;
            style_code = db.ItemMsts.Where(p => p.Style_Code.Contains(term)).Select(p => p.Style_Code).ToList();
            return Json(style_code, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBrands(string term)
        {
            List<string> brand;
            brand = db.BrandMsts.Where(p => p.Brand_ID.Contains(term)).Select(i => i.Brand_ID.Replace(" ", string.Empty)).ToList();
            return Json(brand, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCats(string term)
        {
            List<string> cat;
            cat = db.CatMsts.Where(p => p.Cat_ID.Contains(term)).Select(i => i.Cat_ID.Replace(" ", string.Empty)).ToList();
            return Json(cat, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPros(string term)
        {
            List<string> brand;
            brand = db.ProdMsts.Where(p => p.Prod_ID.Contains(term)).Select(i => i.Prod_ID.Replace(" ", string.Empty)).ToList();
            return Json(brand, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetJews(string term)
        {
            List<string> brand;
            brand = db.JewelTypeMsts.Where(p => p.JewelTypeMst1.Contains(term)).Select(i => i.JewelTypeMst1.Replace(" ", string.Empty)).ToList();
            return Json(brand, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCers(string term)
        {
            List<string> brand;
            brand = db.CertifyMsts.Where(p => p.Certify_ID.Contains(term)).Select(i => i.Certify_ID.Replace(" ", string.Empty)).ToList();
            return Json(brand, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMetalID(string term)
        {
            List<string> brand;
            brand = db.GoldKrtMsts.Where(p => p.GoldType_ID.Contains(term)).Select(i => i.GoldType_ID.Replace(" ", string.Empty)).ToList();
            return Json(brand, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStoneID(string term)
        {
            List<string> brand;
            brand = db.StoneQltyMsts.Where(p => p.StoneQlty_ID.Contains(term)).Select(i => i.StoneQlty_ID.Replace(" ", string.Empty)).ToList();
            return Json(brand, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDiamondI(string term)
        {
            List<string> item;
            item = db.DimQltyMsts.Where(p => p.DimQlty_ID.Contains(term)).Select(i => i.DimQlty_ID.Replace(" ", string.Empty)).ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDiamondID(string term)
        {
            List<string> item;
            item = db.DimQltySubMsts.Where(p => p.DimSubType_ID.Contains(term)).Select(i => i.DimSubType_ID.Replace(" ", string.Empty)).ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion Autocomplete

        #endregion Product
    }

}
