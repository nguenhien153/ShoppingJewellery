using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingJewellery.Models
{
    public class Account
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        public AdminLoginMst Get(string username) {
            return db.AdminLoginMsts.Single(i => i.Username.Equals(username));
        }

    }
}