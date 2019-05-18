using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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


        #region Product
        public List<ProductView> GetProduct()
        {
            return db.ProductViews.OrderBy(p=>p.Prod_Type).ToList();
        }

        public int EditProdductInfor(ItemMst item, string image)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var pro = dbb.ItemMsts.Where(p => p.Style_Code.Replace(" ", string.Empty).Equals(item.Style_Code.Replace(" ", string.Empty))).FirstOrDefault<ItemMst>();
                if (pro != null)
                {
                    pro.Img = image;

                    if (dbb.CatMsts.Any(p => p.Cat_ID.Replace(" ", string.Empty).Equals(item.Cat_ID)))
                    {
                        pro.Cat_ID = item.Cat_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("cat");
                    }

                    if (dbb.CertifyMsts.Any(p => p.Certify_ID.Replace(" ", string.Empty).Equals(item.Certify_ID)))
                    {
                        pro.Certify_ID = item.Certify_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("cer");
                    }

                    if (dbb.JewelTypeMsts.Any(p => p.JewelTypeMst1.Replace(" ", string.Empty).Equals(item.JewelleryType_ID)))
                    {
                        pro.JewelleryType_ID = item.JewelleryType_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("jew");
                    }

                    if (dbb.BrandMsts.Any(p => p.Brand_ID.Replace(" ", string.Empty).Equals(item.Brand_ID)))
                    {
                        pro.Brand_ID = item.Brand_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("brand");
                    }

                    if (dbb.ProdMsts.Any(p => p.Prod_ID.Replace(" ", string.Empty).Equals(item.Prod_ID)))
                    {
                        pro.Prod_ID = item.Prod_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("pro");
                    }

                    pro.Gold_Making = item.Gold_Making;
                    pro.MRP = item.MRP;
                    pro.Net_Gold = item.Net_Gold;
                    pro.Other_Making = item.Other_Making;
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

        public int EditProdductInfor(ItemMst item)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var pro = dbb.ItemMsts.Where(p => p.Style_Code.Replace(" ", string.Empty).Equals(item.Style_Code.Replace(" ", string.Empty))).FirstOrDefault<ItemMst>();
                if (pro != null)
                {
                    if (dbb.CatMsts.Any(p => p.Cat_ID.Replace(" ", string.Empty).Equals(item.Cat_ID)))
                    {
                        pro.Cat_ID = item.Cat_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("cat");
                    }

                    if (dbb.CertifyMsts.Any(p => p.Certify_ID.Replace(" ", string.Empty).Equals(item.Certify_ID)))
                    {
                        pro.Certify_ID = item.Certify_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("cer");
                    }

                    if (dbb.JewelTypeMsts.Any(p => p.JewelTypeMst1.Replace(" ", string.Empty).Equals(item.JewelleryType_ID)))
                    {
                        pro.JewelleryType_ID = item.JewelleryType_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("jew");
                    }

                    if (dbb.BrandMsts.Any(p => p.Brand_ID.Replace(" ", string.Empty).Equals(item.Brand_ID)))
                    {
                        pro.Brand_ID = item.Brand_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("brand");
                    }

                    if (dbb.ProdMsts.Any(p => p.Prod_ID.Replace(" ", string.Empty).Equals(item.Prod_ID)))
                    {
                        pro.Prod_ID = item.Prod_ID;
                    }
                    else
                    {
                        throw new ArgumentNullException("pro");
                    }

                    pro.Gold_Making = item.Gold_Making;
                    pro.MRP = item.MRP;
                    pro.Net_Gold = item.Net_Gold;
                    pro.Other_Making = item.Other_Making;
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

        public int ProductInforCreate(ItemMst item, string filename)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                if (!dbb.ItemMsts.Any(p=>p.Style_Code.Replace(" ",string.Empty).ToLower().Equals(item.Style_Code.Replace(" ",string.Empty).ToLower())))
                {
                    if (!dbb.BrandMsts.Any(p => p.Brand_ID.Replace(" ", string.Empty).Equals(item.Brand_ID)))
                    {
                        throw new ArgumentNullException("brandFalse");
                    }

                    if (!dbb.CatMsts.Any(p => p.Cat_ID.Replace(" ", string.Empty).Equals(item.Cat_ID)))
                    {
                        throw new ArgumentNullException("catFalse");
                    }

                    if (!dbb.ProdMsts.Any(p => p.Prod_ID.Replace(" ", string.Empty).Equals(item.Prod_ID)))
                    {
                        throw new ArgumentNullException("proFalse");
                    }

                    if (!dbb.CertifyMsts.Any(p => p.Certify_ID.Replace(" ", string.Empty).Equals(item.Certify_ID)))
                    {
                        throw new ArgumentNullException("cerFalse");
                    }

                    if (!dbb.JewelTypeMsts.Any(p => p.JewelTypeMst1.Replace(" ", string.Empty).Equals(item.JewelleryType_ID)))
                    {
                        throw new ArgumentNullException("jewFalse");
                    }

                    item.Img = filename;
                    dbb.ItemMsts.Add(item);
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

        public string GetProtype(string id)
        {
            using (JewelleryShopping_dbEntities dbb  = new JewelleryShopping_dbEntities())
            {
                var Protype = dbb.ProdMsts.Find(id);
                return Protype.Prod_Type;
            }
        }

        #region Metal

        public List<GoldView> GetOptionGold(string style_code)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.GoldViews.Where(p => p.Style_code.Replace(" ", string.Empty).Equals(style_code)).ToList();
                return item;
            }
        }

        public int MetalOptionEdit(GoldMst gold)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.GoldMsts.Find(gold.ID);
                if (item != null)
                {
                    if (!dbb.GoldKrtMsts.Any(p => p.GoldType_ID.Replace(" ", string.Empty).Equals(gold.GoldType_ID)))
                    {
                        throw new ArgumentNullException();
                    }
                    if (item.No != gold.No)
                    {
                        if (dbb.GoldMsts.Any(p => p.Style_code.Replace(" ", string.Empty).Equals(item.Style_code.Replace(" ", string.Empty)) && p.No == gold.No))
                        {
                            throw new DbUpdateException();
                        }
                        else
                        {
                            item.No = gold.No;

                        }
                    }
                    item.GoldType_ID = gold.GoldType_ID;
                    item.Total_weight = gold.Total_weight;
                    item.Wstg_Per = gold.Wstg_Per;
                    item.Wstq = gold.Wstq;
                    item.Gold_Wt = gold.Gold_Wt;
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int MetalOptionCreate(GoldMst gold)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.ItemMsts.Where(p => p.Style_Code.Replace(" ", string.Empty).ToLower().Equals(gold.Style_code.Replace(" ", string.Empty).ToLower())).FirstOrDefault<ItemMst>();
                if (item != null)
                {

                    if (!dbb.GoldKrtMsts.Any(p => p.GoldType_ID.Replace(" ", string.Empty).Equals(gold.GoldType_ID.Replace(" ", string.Empty))))
                    {
                        throw new ArgumentException();
                    }

                    if (dbb.GoldMsts.Any(p => p.Style_code.Replace(" ", string.Empty).ToLower().Equals(gold.Style_code.Replace(" ", string.Empty).ToLower()) && p.No == gold.No))
                    {
                        throw new DbUpdateException();
                    }
                    dbb.GoldMsts.Add(gold);
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int MetalOptionDetele(int id)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.GoldMsts.Find(id);
                if (item != null)
                {
                    dbb.GoldMsts.Remove(item);
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

        }
        #endregion Metal

        #region Stone

        public List<StoneView> GetOptionStone(string style_code)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.StoneViews.Where(p => p.Style_Code.Replace(" ", string.Empty).Equals(style_code)).ToList<StoneView>();
                return item;
            }
        }

        public StoneMst GetStone(int id)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.StoneMsts.Find(id);
                return item;
            }
        }

        public int EditOptionStone(StoneMst stone)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.StoneMsts.Find(stone.ID);
                if (item != null)
                {
                    if (!dbb.StoneQltyMsts.Any(p => p.StoneQlty_ID.Replace(" ", string.Empty).Equals(stone.StoneQlty_ID)))
                    {
                        throw new ArgumentNullException();
                    }
                    if (item.No != stone.No)
                    {
                        if (dbb.StoneMsts.Any(p => p.Style_Code.Replace(" ", string.Empty).Equals(item.Style_Code.Replace(" ", string.Empty)) && p.No == stone.No))
                        {
                            throw new DbUpdateException();
                        }
                        else
                        {
                            item.No = stone.No;

                        }
                    }
                    item.StoneQlty_ID = stone.StoneQlty_ID;
                    item.Stone_Gm = stone.Stone_Gm;
                    item.Stone_Pcs = stone.Stone_Pcs;
                    item.Stone_Crt = stone.Stone_Crt;
                    item.Stone_Rate = stone.Stone_Rate;
                    item.Stone_Amt = stone.Stone_Amt;
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int CreateOptionStone(StoneMst stone)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.ItemMsts.Where(p => p.Style_Code.Replace(" ", string.Empty).ToLower().Equals(stone.Style_Code.Replace(" ", string.Empty).ToLower())).FirstOrDefault<ItemMst>();
                if (item != null)
                {
                    if (!dbb.StoneMsts.Any(p => p.StoneQlty_ID.Replace(" ", string.Empty).Equals(stone.StoneQlty_ID.Replace(" ", string.Empty))))
                    {
                        throw new ArgumentException();
                    }
                    if (dbb.StoneMsts.Any(p => p.Style_Code.Replace(" ", string.Empty).ToLower().Equals(stone.Style_Code.Replace(" ", string.Empty).ToLower()) && p.No == stone.No))
                    {
                        throw new DbUpdateException();
                    }
                    dbb.StoneMsts.Add(stone);
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int DeleteOptionStone(int id)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.StoneMsts.Find(id);
                if (item != null)
                {
                    dbb.StoneMsts.Remove(item);
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        #endregion Stone

        #region Diamond

        public List<DimMst> GetOptionDiamond(string style_code)
        {
            using (JewelleryShopping_dbEntities dbb =new JewelleryShopping_dbEntities())
            {
                var item = dbb.DimMsts.Where(p => p.Style_Code.Replace(" ", string.Empty).Equals(style_code)).ToList<DimMst>();
                return item;
            }
        }

        public DimMst GetDiamond(int id)
        {
            using (JewelleryShopping_dbEntities dbb =new JewelleryShopping_dbEntities())
            {
                var item = dbb.DimMsts.Find(id);
                return item;
            }
        }

        public int EditOptionDiamond(DimMst diamond)
        {
            using (JewelleryShopping_dbEntities dbb =new JewelleryShopping_dbEntities())
            {
                var item = dbb.DimMsts.Find(diamond.ID);
                if (item !=null)
                {
                    if (dbb.DimQltyMsts.Any(p => p.DimQlty_ID.Replace(" ", string.Empty).Equals(diamond.DimQlty_ID))==false)
                    {
                        throw new ArgumentNullException("mainFalse");
                    }
                    if (dbb.DimQltySubMsts.Any(p => p.DimSubType_ID.Replace(" ", string.Empty).Equals(diamond.DimSubType_ID))==false)
                    {
                        throw new ArgumentNullException("subFalse");
                    }
                    if (item.No != diamond.No)
                    {
                        if (dbb.DimMsts.Any(p => p.Style_Code.Replace(" ", string.Empty).Equals(item.Style_Code.Replace(" ", string.Empty)) && p.No == diamond.No))
                        {
                            throw new DbUpdateException();
                        }
                        else
                        {
                            item.No = diamond.No;
                        }
                    }

                    item.DimQlty_ID = diamond.DimQlty_ID;
                    item.Dim_Crt = diamond.Dim_Crt;
                    item.Dim_Pcs = diamond.Dim_Pcs;
                    item.Dim_Gm = diamond.Dim_Gm;
                    item.Dim_Size = diamond.Dim_Size;
                    item.Dim_Rate = diamond.Dim_Rate;
                    item.Dim_Amt = diamond.Dim_Amt;

                    item.DimSubType_ID = diamond.DimSubType_ID;
                    item.Dim_CrtSub = diamond.Dim_CrtSub;
                    item.Dim_PcsSub = diamond.Dim_PcsSub;
                    item.Dim_GmSub = diamond.Dim_GmSub;
                    item.Dim_SizeSub = diamond.Dim_SizeSub;
                    item.Dim_RateSub = diamond.Dim_RateSub;
                    item.Dim_AmtSub = diamond.Dim_AmtSub;

                    item.Total_AmtAll = diamond.Total_AmtAll;
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int CreateOptionDiamond(DimMst diamond)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                if (dbb.ItemMsts.Any(p=> p.Style_Code.Replace(" ", string.Empty).ToLower().Equals(diamond.Style_Code.Replace(" ", string.Empty).ToLower())))
                {
                    if (dbb.DimQltyMsts.Any(p => p.DimQlty_ID.Replace(" ", string.Empty).Equals(diamond.DimQlty_ID)) == false)
                    {
                        throw new ArgumentNullException("mainFalse");
                    }
                    if (dbb.DimQltySubMsts.Any(p => p.DimSubType_ID.Replace(" ", string.Empty).Equals(diamond.DimSubType_ID)) == false)
                    {
                        throw new ArgumentNullException("subFalse");
                    }
                    if (dbb.DimMsts.Any(p => p.Style_Code.Replace(" ", string.Empty).ToLower().Equals(diamond.Style_Code.Replace(" ", string.Empty).ToLower()) && p.No == diamond.No))
                    {
                        throw new DbUpdateException();
                    }
                    dbb.DimMsts.Add(diamond);
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int DeleteOptionDiamond(int id)
        {
            using (JewelleryShopping_dbEntities dbb = new JewelleryShopping_dbEntities())
            {
                var item = dbb.DimMsts.Find(id);
                if (item !=null)
                {
                    dbb.DimMsts.Remove(item);
                    dbb.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        #endregion Diamond

        #endregion Product

    }

    public class Details_Product
    {
        public ProdMst protypee { get; set; }
        public ItemMst pro_item { get; set; }
        public List<GoldView> gold_option { get; set; }
        public List<StoneView> stone_option { get; set; }
        public List<DimMst> diamond_option { get; set; }
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