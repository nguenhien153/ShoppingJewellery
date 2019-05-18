using ShoppingJewellery.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace ShoppingJewellery.Controllers
{
    public class TestController : Controller
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        // GET: Test
        public ActionResult Index()
        {
            return View(db.UserRegMsts.ToList());
        }

        public JsonResult GetBrand(string brandid)
        {
            JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities();
            List<string> brand;
            brand = dbb.ItemMsts.Where(p => p.Style_Code.Contains(brandid)).Select(i => i.Style_Code).ToList();
            return new JsonResult { Data = brand, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Autocomplete(string term)
        {
            JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities();
            List<string> brand;
            brand = dbb.ItemMsts.Where(p => p.Style_Code.StartsWith(term)).Select(i => i.Style_Code).ToList();

            return Json(brand, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index2()
        {
          
            return View();
        }


        public ActionResult Index3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index3(HttpPostedFileBase videofile)
        {
            if (videofile != null)
            {
                string filename = Path.GetFileName(videofile.FileName);
                if (videofile.ContentLength<104857600)
                {
                    videofile.SaveAs(Server.MapPath("/Videofiles/"+filename));

                    string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection sqlconn = new SqlConnection(mainconn);
                    string sqlquery = "insert into [dbo].[videofiles] values (@Vname,@Vpath)";
                    SqlCommand sqlcomm = new SqlCommand(sqlquery,sqlconn);
                    sqlconn.Open();
                    sqlcomm.Parameters.AddWithValue("@Vname", filename);
                    sqlcomm.Parameters.AddWithValue("@Vpath", "/Videofiles/" + filename);
                    sqlcomm.ExecuteNonQuery();
                    sqlconn.Close();

                    ViewData["Message"] = "Record Saved Successfully !";
                }
            }
            return RedirectToAction("Index3"); 
        }
    }
}