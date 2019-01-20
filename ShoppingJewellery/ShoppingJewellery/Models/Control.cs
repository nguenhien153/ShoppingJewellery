using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingJewellery.Models
{
    public class Control
    {
        JewelleryShopping_dbEntities db = new JewelleryShopping_dbEntities();
        public AdminLoginMst GetAd(string username) {
            return db.AdminLoginMsts.Single(i => i.Username.Equals(username));
        }

        public UserRegMst GetUser(string username)
        {
            return db.UserRegMsts.Single(i => i.userID.Equals(username));
        }

        public Feedback GetByID(int id)
        {
            return db.Feedbacks.Single<Feedback>(i => i.FId == id);
        }

        public IEnumerable<Feedback> GetByName(string username) {
            return db.Feedbacks.Where(i => i.Name.Equals(username)).ToList();
        }
    }
}