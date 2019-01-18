using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingJewellery.Models
{
    public class Account
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        public AdminLoginMst GetAd(string username) {
            return db.AdminLoginMsts.Single(i => i.Username.Equals(username));
        }

        public UserRegMst GetUser(string username)
        {
            return db.UserRegMsts.Single(i => i.userID.Equals(username));
        }

    }
}