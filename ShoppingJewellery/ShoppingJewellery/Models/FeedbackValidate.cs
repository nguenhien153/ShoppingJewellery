using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShoppingJewellery.Models
{
    public class FeedbackValidate
    {
        [Key]
        public int FId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Sent date")]
        public DateTime cDate { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage ="Comment must not null")]
        public string Comment { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Reply date")]
        public DateTime ReDate { get; set; }
        
        public string ReComment { get; set; }
    }
    [MetadataType(typeof(FeedbackValidate))]
    public partial class Feedback { }
}