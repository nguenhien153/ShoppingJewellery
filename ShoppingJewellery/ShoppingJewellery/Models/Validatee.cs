using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingJewellery.Models
{
    public class ValidateItem
    {
        [Key]
        [Required(ErrorMessage = "Style code must not blank")]
        [StringLength(50, ErrorMessage = "Style code must less 50 charcter")]
        [Display(Name = "Style Code")]
        public string Style_Code { get; set; }

        [Required(ErrorMessage = "Image must not blank")]
        [StringLength(50, ErrorMessage = "Image must less 50 charcter")]
        [Display(Name = "Image")]
        public string Img { get; set; }


        [Required(ErrorMessage = "Pairs must not blank")]
        [Range(0, 999, ErrorMessage = "Pairs between 0 - 999")]
        [Display(Name = "Pairs")]
        public decimal Pairs { get; set; }

        [Required(ErrorMessage = "Product quality must not blank")]
        [StringLength(50, ErrorMessage = "Product quality must less 50 charcter")]
        [Display(Name = "Product Quality")]
        public string Prod_Quality { get; set; }

        [Required(ErrorMessage = "Quantity must not blank")]
        [Range(0, 999999999999999999, ErrorMessage = "Quantity between 0 - 999999999999999999")]
        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Brand id must not blank")]
        [StringLength(10, ErrorMessage = "Brand id must less 50 charcter")]
        [Display(Name = "Brand ID")]
        public string Brand_ID { get; set; }

        [Required(ErrorMessage = "Category id must not blank")]
        [StringLength(10, ErrorMessage = "Category id must less 50 charcter")]
        [Display(Name = "Category ID")]
        public string Cat_ID { get; set; }


        [Required(ErrorMessage = "Certify id must not blank")]
        [StringLength(10, ErrorMessage = "Certify id must less 50 charcter")]
        [Display(Name = "Certify ID")]
        public string Certify_ID { get; set; }

        [Required(ErrorMessage = "Product id must not blank")]
        [StringLength(10, ErrorMessage = "Product id must less 50 charcter")]
        [Display(Name = "Product ID")]
        public string Prod_ID { get; set; }

        [Required(ErrorMessage = "JewelleryType id must not blank")]
        [StringLength(10, ErrorMessage = "JewelleryType id must less 50 charcter")]
        [Display(Name = "JewelleryType ID")]
        public string JewelleryType_ID { get; set; }

        [Required(ErrorMessage = "Net gold must not blank")]
        [Range(0, 9999999999.99, ErrorMessage = "Net gold between 0 - 9999999999.99")]
        [Display(Name = "Net Gold")]
        public decimal Net_Gold { get; set; }

        [Required(ErrorMessage = "Stone Weight must not blank")]
        [Range(0, 9999999999.999, ErrorMessage = "Stone Weight between 0 - 9999999999.999")]
        [Display(Name = "Stone Weight")]
        public decimal Stone_Wt { get; set; }

        [Required(ErrorMessage = "Total Gross Weight must not blank")]
        [Range(0, 9999999999.99, ErrorMessage = "Total Gross Weight between 0 - 9999999999.99")]
        [Display(Name = "Total Gross Weight")]
        public decimal Tot_Gross_Wt { get; set; }

        [Required(ErrorMessage = "Stone Making  must not blank")]
        [Range(0, 9999999999.99, ErrorMessage = "Stone Making  between 0 - 9999999999.99")]
        [Display(Name = "Stone Making")]
        public decimal Stone_Making { get; set; }

        [Required(ErrorMessage = "Gold Making must not blank")]
        [Range(0, 9999999999.99, ErrorMessage = "Gold Making between 0 - 9999999999.99")]
        [Display(Name = "Gold Making")]
        public decimal Gold_Making { get; set; }

        [Required(ErrorMessage = "Other Making  must not blank")]
        [Range(0, 9999999999.99, ErrorMessage = "Other Making  between 0 - 9999999999.99")]
        [Display(Name = "Other Making")]
        public decimal Other_Making { get; set; }

        [Required(ErrorMessage = "Total Making  must not blank")]
        [Range(0, 9999999999.99, ErrorMessage = "Total Making  between 0 - 9999999999.99")]
        [Display(Name = "Total Making")]
        public decimal Tot_Making { get; set; }

        [Required(ErrorMessage = "Size must not blank")]
        [Range(0, 99, ErrorMessage = "Size between 0 - 99")]
        [Display(Name = "Size")]
        public int size { get; set; }

        [Required(ErrorMessage = "MRP must not blank")]
        [Range(0, 9999999999.99, ErrorMessage = "MRP  between 0 - 9999999999.99")]
        [Display(Name = "MRP")]
        public decimal MRP { get; set; }


    }
    [MetadataType(typeof(ValidateItem))]
    public partial class ItemMst { }

    public class validate_brand
    {
        [Key]
        [Required(ErrorMessage = "Brand id must not blank")]
        [StringLength(10,ErrorMessage ="Brand Id: Max lenght 10 characters")]
        [MinLength(10,ErrorMessage ="Brand Id must have 10 characters")]
        [Display(Name = "Brand ID")]
        public string Brand_ID { get; set; }

        [Required(ErrorMessage ="Brand type must not blank")]
        [StringLength(50, ErrorMessage = "Brand type: Max lenght 50 character")]
        [Display(Name = "Brand Type")]
        public string Brand_Type { get; set; }
    }
    [MetadataType(typeof(validate_brand))]
    public partial class BrandMst { }

    public class validate_cat
    {
        [Key]
        [Required(ErrorMessage = "Category id must not blank")]
        [MinLength(10,ErrorMessage ="Category Id must have 10 characters")]
        [StringLength(10, ErrorMessage = "Category Id: Max lenght 10 character")]
        [Display(Name = "Category ID")]
        public string Cat_ID { get; set; }

        [Required(ErrorMessage = "Category type must not blank")]
        [StringLength(50, ErrorMessage = "Category type: Max lenght 50 character")]
        [Display(Name = "Category Type")]
        public string Cat_Name { get; set; }
    }
    [MetadataType(typeof(validate_cat))]
    public partial class CatMst { }

    public class validate_cer
    {
        [Key]
        [Required(ErrorMessage = "Certify id must not blank")]
        [MinLength(10, ErrorMessage = "Certify Id must have 10 characters")]
        [StringLength(10, ErrorMessage = "Certify Id: Max lenght 10 character")]
        [Display(Name = "Certify ID")]
        public string Certify_ID { get; set; }

        [Required(ErrorMessage = "Certify type must not blank")]
        [StringLength(50, ErrorMessage = "Certify type: Max lenght 50 character")]
        [Display(Name = "Category Type")]
        public string Certify_Type { get; set; }
    }
    [MetadataType(typeof(validate_cer))]
    public partial class CertifyMst { }

    public class validate_pro
    {
        [Key]
        [Required(ErrorMessage = "Product id must not blank")]
        [MinLength(10, ErrorMessage = "Product Id must have 10 characters")]
        [StringLength(10, ErrorMessage = "Product Id: Max lenght 10 character")]
        [Display(Name = "Product ID")]
        public string Prod_ID { get; set; }

        [Required(ErrorMessage = "Product type must not blank")]
        [StringLength(50, ErrorMessage = "Product type: Max lenght 50 character")]
        [Display(Name = "Product Type")]
        public string Prod_Type { get; set; }

        [Required(ErrorMessage = "Category id must not blank")]
        [StringLength(10, ErrorMessage = "Certify Id: Max lenght 10 character")]
        [Display(Name = "Category ID")]
        public string Cat_ID { get; set; }
    }
    [MetadataType(typeof(validate_pro))]
    public partial class ProdMst { }

    public class validate_jew
    {
        [Key]
        [Required(ErrorMessage = "JewelTypeMst id must not blank")]
        [MinLength(10, ErrorMessage = "JewelTypeMst Id must have 10 characters")]
        [StringLength(10, ErrorMessage = "JewelTypeMst Id: Max lenght 10 character")]
        [Display(Name = "JewelTypeMst ID")]
        public string JewelTypeMst1 { get; set; }

        [Required(ErrorMessage = "JewelTypeMst type must not blank")]
        [StringLength(50, ErrorMessage = "JewelTypeMst type: Max lenght 50 character")]
        [Display(Name = "JewelTypeMst Type")]
        public string Jewellery_Type { get; set; }

        [Required(ErrorMessage = "Product id must not blank")]
        [StringLength(10, ErrorMessage = "Product Id: Max lenght 10 character")]
        [Display(Name = "Product ID")]
        public string ID_Prod { get; set; }
    }
    [MetadataType(typeof(validate_jew))]
    public partial class JewelTypeMst { }
}
