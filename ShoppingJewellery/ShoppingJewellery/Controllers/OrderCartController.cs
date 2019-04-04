using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comon;
using ShoppingJewellery.Models;
namespace ShoppingJewellery.Controllers
{
    public class OrderCartController : Controller
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        eProjectDAO dao = new eProjectDAO();
        // GET: OrderCart
        public ActionResult Index()
        {
            return View();
        }

        public int isExsisting(string Style_code, int size, int gold, int diamond, int stone)
        {
            List<ItemCart> cart = (List<ItemCart>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Itemm.Style_Code.Trim().ToLower().Equals(Style_code.Trim().ToLower()) && cart[i].Size1 == size && cart[i].GoldNo1 == gold && cart[i].DiamondNo1 == diamond && cart[i].StoneNo1 == stone)
                {
                    return i;
                }
            }
            return -1;
        }

        public ActionResult Delete(string Style_code, int size, int gold, int diamond, int stone)
        {
            int index = isExsisting(Style_code, size, gold, diamond, stone);
            List<ItemCart> cart = (List<ItemCart>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Index");
        }

        public ActionResult CartShop(string Style_code, int size, int gold, int diamond, int stone, decimal unitprice, string img, string Namemetal, string Namediamond, string Namestone)
        {
            if (Session["cart"] == null)
            {
                List<ItemCart> cart = new List<ItemCart>();
                cart.Add(new ItemCart(db.ItemMsts.First(item => item.Style_Code.Trim().ToLower().Equals(Style_code.Trim().ToLower())), 1, diamond, gold, stone, size, unitprice, img, Namemetal, Namediamond, Namestone));
                Session["cart"] = cart;
            }
            else
            {
                List<ItemCart> cart = (List<ItemCart>)Session["cart"];
                int index = isExsisting(Style_code, size, gold, diamond, stone);
                if (index == -1)
                {
                    cart.Add(new ItemCart(db.ItemMsts.First(item => item.Style_Code.Trim().ToLower().Equals(Style_code.Trim().ToLower())), 1, diamond, gold, stone, size, unitprice, img, Namemetal, Namediamond, Namestone));
                }
                else
                {
                    cart[index].Quantity++;
                    Session["cart"] = cart;
                }
            }
            return View();
        }

        public ActionResult Checkout()
        {
            if (Session["cart"] == null || Session["total"] == null)
            {
                return RedirectToAction("ProductList", "Product");
            }
            return View();

        }

        [HttpPost]
        public ActionResult Checkout(Order_ order)
        {
            if (Session["cart"] == null || Session["total"] == null)
            {
                return RedirectToAction("ProductList", "Product");
            }
            Order_ orderrs = new Order_();
            orderrs = order;
            Session["Order"] = orderrs;
            return RedirectToAction("Infor_Order");
        }

        public ActionResult Payment()
        {
            if (Session["cart"] == null || Session["total"] == null || Session["Order"] == null)
            {
                return RedirectToAction("ProductList", "Product");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Payment(Payment pay)
        {
            if (Session["cart"] == null || Session["total"] == null || Session["Order"] == null)
            {
                return RedirectToAction("ProductList", "Product");
            }
            try
            {
                Payment payment = new Payment();
                Order_ order = new Order_();
                List<ItemCart> item = (List<ItemCart>)Session["cart"];
                payment = pay;
                order = (Order_)Session["Order"];
                db.Order_.Add(order);
                db.SaveChanges();
                Order_ getorder = db.Order_.Where(demo => demo.FirstName.ToLower().Trim().Equals(order.FirstName.ToLower().Trim()) && demo.LastName.ToLower().Trim().Equals(order.LastName.ToLower().Trim()) && demo.Country.Equals(order.Country) && demo.City.Trim().ToLower().Equals(order.City.ToLower().Trim()) && demo.State_Province.Equals(order.State_Province) && demo.Zip_Postal.Equals(order.Zip_Postal) && demo.Phone.Trim().Equals(order.Phone.Trim()) && demo.email.ToLower().Trim().Equals(order.email.ToLower().Trim()) && demo.Status.Trim().ToLower().Equals(order.Status.Trim().ToLower()) && demo.Address.ToLower().Trim().Equals(order.Address.Trim().ToLower()) && demo.Total_brand == order.Total_brand).OrderByDescending(q => q.ID_Order).FirstOrDefault();
                Order_Details order_details = new Order_Details();
                foreach (var abc in item)
                {
                    order_details.ID_Order = getorder.ID_Order;
                    order_details.Style_code = abc.Itemm.Style_Code.ToString();
                    order_details.Gold_No = (int)abc.GoldNo1;
                    order_details.Diamond_No = (int)abc.DiamondNo1;
                    order_details.Stone_No = (int)abc.StoneNo1;
                    order_details.Size = (int)abc.Size1;
                    order_details.UnitPrice = (decimal)abc.UnitPrice1;
                    order_details.Quantity = (int)abc.Quantity;
                    order_details.Total = (decimal)(abc.UnitPrice1 * abc.Quantity);
                    db.Order_Details.Add(order_details);
                    db.SaveChanges();
                }

                payment.ID_Order = getorder.ID_Order;
                db.Payments.Add(payment);
                db.SaveChanges();

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Layout/Mail/send_paymentsuccess.html"));
                content = content.Replace("{{CustomerName}}", order.FirstName + " " + order.LastName);
                content = content.Replace("{{Order}}",getorder.ID_Order.ToString());
                new MailHelper().SendMail(order.email, "Email Validation Order Of JeweleryShopping", content, "Payment order of JeweleryShopping success");

                Session["Order"] = null;
                Session["cart"] = null;
                Session["total"] = null;
                Session["code"] = null;




                return RedirectToAction("payment_success");
            }
            catch (Exception)
            {
                Session["Order"] = null;
                Session["cart"] = null;
                Session["total"] = null;
                Session["code"] = null;
                return RedirectToAction("payment_false");
            }
        }

        public ActionResult payment_success()
        {
            return View();
        }
        public ActionResult payment_false()
        {
            return View();
        }

        public ActionResult Infor_Order()
        {
            if (Session["cart"] == null || Session["Order"] == null)
            {
                return RedirectToAction("ProductList", "Product");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Step2(Mail_send mail)
        {
            if (Session["cart"] == null || Session["Order"] == null || Session["total"] == null)
            {
                return RedirectToAction("ProductList", "Product");
            }

            try
            {
                Random rnd = new Random();
                int code = rnd.Next(10000, 99999);
                Session["total"] = mail.Total;
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Layout/Mail/verify_mail.html"));
                content = content.Replace("{{CustomerName}}", mail.Fullname);
                content = content.Replace("{{Phone}}", mail.Phone);
                content = content.Replace("{{Address}}", mail.Address);
                content = content.Replace("{{Zip}}", mail.Zip);
                content = content.Replace("{{Date}}", mail.Date);
                content = content.Replace("{{instruction}}", mail.Instruction);
                content = content.Replace("{{Total}}", mail.Total.ToString());
                content = content.Replace("{{Code}}", code.ToString());


                new MailHelper().SendMail(mail.Email.ToString(), "Email Validation Order Of JeweleryShopping", content, "Email Validation Order Of JeweleryShopping");
                TempData["send_code"] = null;

                Session["code"] = code;
            }
            catch (Exception)
            {
                TempData["send_code"] = "<script>alert('Send Code False. Please Check Your Email !!!');</script>";
            }
            return View();
        }


        public ActionResult CheckOrder()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetOrder(int id)
        {
            Session["code_check"] = null;
            Order_ order = db.Order_.Find(id);
            List<Order_Details> list_order = db.Order_Details.Where(demo => demo.ID_Order == id).ToList();
            if (order != null && list_order.Count != 0)
            {

                Session["check_order"] = order;
                Session["list_order"] = list_order;
                Session["diamond"] = db.DiamonViews.ToList();
                Session["stone"] = db.StoneViews.ToList();
                Session["gold"] = db.GoldViews.ToList();
            }
          
            return View();
        }

        public ActionResult send_mail_order(int id, string mail)
        {
            Session["check_order"] = null;
            Session["list_order"] = null;
            Session["diamond"] = null;
            Session["stone"] = null;
            Session["gold"] = null;
            Session["id"] = null;
            Session["code_check"] = null;
            TempData["send_codee"] = null;

            Order_ order = db.Order_.Find(id);
            List<Order_Details> list_order = db.Order_Details.Where(demo => demo.ID_Order == id).ToList();
            if (order != null && order.email.Trim().ToLower().Equals(mail.Trim().ToLower()) && list_order.Count != 0 )
            {
                try
                {
                    string Fullname = order.FirstName +" "+ order.LastName;
                    Random rnd = new Random();
                    int code = rnd.Next(10000, 99999);
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Layout/Mail/veriify_checkorder.html"));
                    content = content.Replace("{{CustomerName}}", Fullname);
                    content = content.Replace("{{Code}}", code.ToString());


                    new MailHelper().SendMail(mail, "Email Send Code Validate Check Order", content, "Email Validation Check Order Of JeweleryShopping");
                    Session["code_check"] = code;
                    TempData["send_codee"] = "<script>alert('Code has been send !!!');</script>";
                    Session["id"] = id;
                }
                catch (Exception)
                {
                    Session["id"] = null ;
                    TempData["send_codee"] = "<script>alert('Send Code False. Please Check Your Email !!!');</script>";
                }
            }
            else
            {
                TempData["send_codee"] = "<script>alert('ID order Or Your Email not valid !!!');</script>";
            }

            return View();
        }
    }
}