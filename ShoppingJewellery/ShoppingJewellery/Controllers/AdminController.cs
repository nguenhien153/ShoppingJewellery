using ShoppingJewellery.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
namespace ShoppingJewellery.Controllers
{
    public class AdminController : Controller
    {
        Adminn dao = new Adminn();
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        // GET: Admin
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
        // Begin Account ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
        //end Account----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // User----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

        // end User Acount--------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // User Address
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
        //user address -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //product --------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
        public ActionResult Product()
        {
            return View(dao.GetProduct());
        }

        public ActionResult Product_Details(string style_code)
        {
            var item = dao.GetItem_Pro(style_code);
            var protype = dao.GetProductType(item.Prod_ID);
            if (item != null && protype != null)
            {
                Details_Product jewel = new Details_Product();
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
            if (imagefile != null)
            {
                string filename = Path.GetFileName(imagefile.FileName);
                if (imagefile.ContentLength < 10485760)
                {
                    imagefile.SaveAs(Server.MapPath("/Content/images/Product_Image/" + Protype + "/Img/" + filename));
                }

                int check = dao.EditProdductInfor(item, filename);
                if (check == 0)
                {
                    Session["Product_Details"] = "success";
                    return RedirectToAction("Product_Details", "Admin", new { style_code = item.Style_Code.Replace(" ", string.Empty) });
                }
                else if (check == 1)
                {
                    Session["Product_Details"] = "false";
                    return RedirectToAction("Product_Details", "Admin", new { style_code = item.Style_Code.Replace(" ", string.Empty) });

                }
                else
                {
                    Session["Product_Details"] = "error";
                    return RedirectToAction("Product_Details", "Admin", new { style_code = item.Style_Code.Replace(" ", string.Empty) });

                }
            }
            return RedirectToAction("Product");
        }

    }

}
