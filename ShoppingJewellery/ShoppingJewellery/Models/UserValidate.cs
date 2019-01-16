using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShoppingJewellery.Models
{
    public class UserValidate
    {
        [Key]
        [StringLength(10, ErrorMessage = "user name must less 10 charcter")]
        [Display(Name = "Username")]
        public string userID { get; set; }

        [Required(ErrorMessage = "First name must not blank")]
        [StringLength(50, ErrorMessage = "First name must less 50 charcter")]
        [Display(Name = "First name")]
        public string userFname { get; set; }

        [Required(ErrorMessage = "Last name must not blank")]
        [StringLength(50, ErrorMessage = "Last name must less 50 charcter")]
        [Display(Name = "Last name")]
        public string userLname { get; set; }

        [Required(ErrorMessage = "Phone must not blank")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "Phone must  8-10 number")]
        [Phone]
        public string mobNo { get; set; }

        [Required(ErrorMessage = "Email must not blank")]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "email must less 50 charcter")]
        public string emailID { get; set; }

        [Required(ErrorMessage = "birthday must not blank")]
        [DataType(DataType.Date)]
        public string dob { get; set; }

        [DataType(DataType.Date)]
        public string cdate { get; set; }

        [Required(ErrorMessage = "Password must not blank")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Password must less 7-50 character")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    [MetadataType(typeof(UserValidate))]
    public partial class UserRegMst { }
}