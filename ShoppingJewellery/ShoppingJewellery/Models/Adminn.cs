using System;
using System.Collections.Generic;
using System.Linq;
namespace ShoppingJewellery.Models
{
    public class Adminn
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();

        public string Verify_Login(string password, string username)
        {
            var user = db.AdminLoginMsts.Where(deo => deo.Username.Trim().Equals(username.Trim()) && deo.Password.Trim().Equals(password.Trim())).FirstOrDefault();

            if (user != null)
            {
                if (user.role == true)
                {
                    return "Super_Admin";
                }
                else
                {
                    return "Admin";
                }

            }
            else
            {
                return "false";
            }
        }


        // Begin Account
        public string GetUsernameSuperAdmin()
        {
            var admin = db.AdminLoginMsts.First(demo => demo.role == true);
            if (admin != null)
            {
                return admin.Username;
            }
            else
            {
                return "null";
            }
        }
        public List<AdminLoginMst> GetAdminLogins()
        {
            List<AdminLoginMst> admin = db.AdminLoginMsts.OrderByDescending(demo => demo.role).ToList();
            return admin;
        }

        public AdminLoginMst GetAdmin(string username)
        {
            var admin = db.AdminLoginMsts.Find(username);
            return admin;
        }

        public bool ChangePasswordAdmin(AdminLoginMst admin)
        {
            var adm = GetAdmin(admin.Username);
            if (adm != null)
            {
                adm.Password = admin.Password;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAdminAccount(string username)
        {
            var admin = db.AdminLoginMsts.Find(username);
            if (admin != null)
            {
                db.AdminLoginMsts.Remove(admin);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CreateAdminAccount(AdminLoginMst admin)
        {
            try
            {
                var demo = db.AdminLoginMsts.Find(admin.Username);
                if (demo == null)
                {
                    db.AdminLoginMsts.Add(admin);
                    db.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }

            }
            catch (Exception)
            {
                return 2;
            }
        }
        //end Admin


        //User Account
        public List<UserRegMst> GetUser()
        {
            return db.UserRegMsts.ToList();
        }
        public UserRegMst GetSingleUser(string username)
        {
            var user = db.UserRegMsts.Find(username);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public int EditUserAccount(UserRegMst user)
        {
            try
            {
                UserRegMst userr = db.UserRegMsts.Find(user.userID);
                if (userr != null)
                {
                    userr.userLname = user.userLname;
                    userr.userFname = user.userFname;
                    userr.password = user.password;
                    userr.mobNo = user.mobNo;
                    userr.emailID = user.emailID;
                    userr.dob = user.dob;
                    db.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                return 2;
            }
        }

        public int DeleteUserAccount(string username)
        {

            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                try
                {
                    var userr = dbb.UserRegMsts.Find(username);
                    if (userr != null)
                    {
                        if (userr.Addresses != null)
                        {
                            foreach (Address addr in userr.Addresses.ToList())
                            {
                                dbb.Addresses.Remove(addr);
                            }
                        }
                        dbb.UserRegMsts.Remove(userr);
                        dbb.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception)
                {

                    return 2;
                }
            }
        }

        public int CreateUserAccount(UserRegMst user)
        {
            try
            {
                var userr = db.UserRegMsts.Find(user.userID);
                if (userr == null)
                {
                    db.UserRegMsts.Add(user);
                    db.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {

                return 2;
            }
        }
        // end User Account


        // User Address 
        public int CreateUserAddress(Address add)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                try
                {
                    var user = dbb.UserRegMsts.Where(p => p.userID.Trim().Equals(add.UserID.Trim())).FirstOrDefault<UserRegMst>();
                    if (user != null)
                    {
                        if (add.default_address == true)
                        {
                            var addr = dbb.Addresses.Where(p => p.default_address == true && p.UserID.Trim().Equals(add.UserID.Trim())).FirstOrDefault<Address>();
                            if (addr != null)
                            {
                                addr.default_address = false;
                                dbb.SaveChanges();
                            }
                        }
                        dbb.Addresses.Add(add);
                        dbb.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception)
                {

                    return 2;
                }
            }
        }

        public int EditUserAddress(Address add)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                try
                {
                    var addr = dbb.Addresses.Find(add.NorR);
                    if (addr != null)
                    {
                        if (add.default_address == true)
                        {
                            if (addr.default_address == false)
                            {
                                var addr2 = dbb.Addresses.Where(p => p.default_address == true && p.UserID.Trim().Equals(add.UserID.Trim())).FirstOrDefault<Address>();
                                if (addr2 != null)
                                {
                                    addr2.default_address = false;
                                    dbb.SaveChanges();
                                }
                            }
                        }
                        addr.Address1 = add.Address1;
                        addr.city = add.city;
                        addr.country = add.country;
                        addr.default_address = add.default_address;
                        addr.Instruction_Optional = add.Instruction_Optional;
                        addr.state_province = add.state_province;
                        addr.Zip_Postal = add.Zip_Postal;
                        dbb.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception)
                {

                    return 2;
                }
            }
        }

        public int DeleteUserAddress(int key)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                try
                {
                    var add = dbb.Addresses.Find(key);
                    if (add != null)
                    {
                        dbb.Addresses.Remove(add);
                        dbb.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
            }
        }
        // endd user Addresss


        // Product
        public List<ProductView> GetProduct()
        {
            return db.ProductViews.ToList();
        }

        public int EditProdductInfor(ItemMst item,string image)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var pro = dbb.ItemMsts.Where(p => p.Style_Code.Replace(" ", string.Empty).Equals(item.Style_Code.Replace(" ",string.Empty))).FirstOrDefault<ItemMst>();
                if (pro!=null)
                {
                    pro.Brand_ID = item.Brand_ID;
                    pro.Cat_ID = item.Cat_ID;
                    pro.Certify_ID = item.Certify_ID;
                    pro.Gold_Making = item.Gold_Making;
                    pro.Img = image;
                    pro.JewelleryType_ID = item.JewelleryType_ID;
                    pro.MRP = item.MRP;
                    pro.Net_Gold = item.Net_Gold;
                    pro.Other_Making = item.Other_Making;
                    pro.Pairs = item.Pairs;
                    pro.Prod_ID = item.Prod_ID;
                    pro.Prod_Quality = item.Prod_Quality;
                    pro.Quantity = item.Quantity;
                    pro.size = item.size;
                    pro.Stone_Making = item.Stone_Making;
                    pro.Stone_Wt = item.Stone_Wt;
                    pro.Tot_Gross_Wt = item.Tot_Gross_Wt;
                    pro.Tot_Making = item.Tot_Making;
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }


        public ItemMst GetItem_Pro(string style_code)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                    var item = dbb.ItemMsts.Where(p => p.Style_Code.Replace(" ", string.Empty).Equals(style_code)).FirstOrDefault<ItemMst>();
                    return item;
            }
        }

        public ProdMst GetProductType(string Prod_ID)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.ProdMsts.Where(p => p.Prod_ID.Equals(Prod_ID)).FirstOrDefault<ProdMst>();
                return item;
            }
        }



    }

    public class Details_Product
    {
        public ProdMst protypee { get; set; }
        public ItemMst pro_item { get; set; }
    }
    public class Objeect
    {
        public Objeect()
        {
            this.Addresses1 = new HashSet<Addresss>();
        }

        public string userID { get; set; }
        public string userLname { get; set; }
        public string userFname { get; set; }
        public string password { get; set; }
        public string mobNo { get; set; }
        public string emailID { get; set; }
        public string dob { get; set; }
        public string cdate { get; set; }
        public virtual ICollection<Addresss> Addresses1 { get; set; }
    }
    public class Addresss
    {
        public string Address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string Instruction_Optional { get; set; }
        public string state_province { get; set; }
        public string Zip_Postal { get; set; }
        public string NorR { get; set; }
        public string default_address { get; set; }
    }
}