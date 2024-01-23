using System.ComponentModel.DataAnnotations;

namespace Project.MVCUI.ViewModels.WrapperClasses
{
    public class OrderWrapper
    {
        public AppUserViewModel AppUser { get; set; } = null!;


        //----------------------------Address Information--------------------
        [Display(Name = "Adres")]
        [Required(ErrorMessage = "{0} seçimi zorunludur")]
        public int AddressId { get; set; }
        public string FullAddress { get; set; } = null!;
        //----------------------------Address Information--------------------




        //----------------------------Card Information--------------------
        [Display(Name = "Kart Sahibinin Adı Soyadı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "{2} ile {1} karakter arasında olmalıdır")]
        [DataType(DataType.Text)]
        public string CardUserName { get; set; } = null!;

        [Display(Name = "Güvenlik Numarası")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Geçersiz format (Ör. 222)")]
        public string SecurityNumber { get; set; } = null!;

        [Display(Name = "Kart Numarası")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "16 karakter olmalıdır")]
        [RegularExpression(@"^\d{4}\d{4}\d{4}\d{4}$", ErrorMessage = "Bu alan sadece rakamlardan oluşmalı ve 16 haneli olmalıdır")]
        public string CardNumber { get; set; } = null!;

        [Display(Name = "S.K. Ay")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "2 karakter olmalıdır")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "Bu alan sadece rakamlardan oluşmalıdır")]
        public string CardExpiryMounth { get; set; } = null!;

        [Display(Name = "S.K. Yıl")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "4 karakter olmalıdır")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Bu alan sadece rakamlardan oluşmalıdır")]
        public string CardExpiryYear { get; set; } = null!;
        public decimal ShoppingPrice { get; set; }
        //----------------------------Card Information--------------------
    }
}
