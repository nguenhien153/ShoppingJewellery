using ShoppingJewellery.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ShoppingJewellery.Server
{
    /// <summary>
    /// Summary description for User_Address
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class User_Address : System.Web.Services.WebService
    {

        [WebMethod]
        public void HelloWorld()
        {

            List<Objeect> supeerr = new List<Objeect>();
            string item2 = " ";
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Get_User_Address", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Objeect obbj = new Objeect();
                    if (item2.Trim().Equals(rdr["userID"].ToString().Trim()))
                    {
                        var objj2 = supeerr.FindIndex(m => m.userID.ToString().Trim().Equals(rdr["userID"].ToString().Trim()));
                        supeerr[objj2].Addresses1.Add(new Addresss {  country = rdr["country"].ToString(), Address = rdr["Address"].ToString(), city = rdr["city"].ToString(), Instruction_Optional = rdr["Instruction_Optional"].ToString(), state_province = rdr["state_province"].ToString(), Zip_Postal = rdr["Zip_Postal"].ToString(), default_address = rdr["default_address"].ToString(),NorR = rdr["NorR"].ToString() });

                    }
                    else
                    {
                        obbj.userID = rdr["userID"].ToString();
                        obbj.userLname = rdr["userLname"].ToString();
                        obbj.userFname = rdr["userFname"].ToString();
                        obbj.password = rdr["password"].ToString();
                        obbj.mobNo = rdr["mobNo"].ToString();
                        obbj.emailID = rdr["emailID"].ToString();
                        obbj.dob = rdr["dob"].ToString();
                        obbj.cdate = rdr["cdate"].ToString();
                        obbj.Addresses1.Add(new Addresss { country = rdr["country"].ToString(), Address = rdr["Address"].ToString(), city = rdr["city"].ToString(), Instruction_Optional = rdr["Instruction_Optional"].ToString(), state_province = rdr["state_province"].ToString(), Zip_Postal = rdr["Zip_Postal"].ToString(), default_address = rdr["default_address"].ToString(),NorR= rdr["NorR"].ToString() });
                        item2 = rdr["userID"].ToString();
                        supeerr.Add(obbj);
                    }

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(supeerr));
        }
    }
}
