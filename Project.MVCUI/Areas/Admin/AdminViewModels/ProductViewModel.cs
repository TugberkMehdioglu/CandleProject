﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class ProductViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        public string Name { get; set; } = null!;

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "{0} zorunludur")]
        public string Description { get; set; } = null!;

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "{0} zorunludur")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,3})$", ErrorMessage = "{0} alanında noktadan sonra en fazla 3 basamak olmalıdır")]
        public decimal Price { get; set; }

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "{0} zorunludur")]
        public string PriceText { get; set; } = null!;//For priceBind on html

        [Display(Name = "Stok")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [Range(1, 100000, ErrorMessage = "{0} alanı {1} ile {2} arasında olmalıdır")]
        [DataType(DataType.Text)]
        public short Stock { get; set; }

        public List<IFormFile>? Images { get; set; }

        [Display(Name = "Ürün Kategorisi")]
        [Required(ErrorMessage = "{0} zorunludur")]
        public int CategoryID { get; set; }

        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        public string? FormerName { get; set; }


        //Navigation Properties

        [ValidateNever]
        public ICollection<OrderDetailViewModel>? OrderDetails { get; set; }

        [ValidateNever]
        public List<PhotoViewModel>? Photos { get; set; }
    }
}
