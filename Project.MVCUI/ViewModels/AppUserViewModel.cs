using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Project.MVCUI.ViewModels
{
    public class AppUserViewModel
    {
        public string? Id { get; set; }

        [Display(Name ="Kullanıcı Adı")]
        [Required(ErrorMessage ="{0} zorunludur")]
        [StringLength(80, MinimumLength = 2, ErrorMessage ="{0}, {2} ile {1} karakter arasında olmalıdır")]
        [DataType(DataType.Text)]
        public string UserName { get; set; } = null!;

        [Display(Name ="Email")]
        [Required(ErrorMessage ="{0} zorunludur")]
        [EmailAddress(ErrorMessage ="Doğru formatta {0} giriniz")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "{0}, {2} ile {1} karakter arasında olabilir")]
        [DataType(DataType.Text)]
        public string Email { get; set; } = null!;

        [Display(Name ="Şifre")]
        [Required(ErrorMessage ="{0} zorunludur")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name ="Şifre Onay")]
        [Required(ErrorMessage ="{0} zorunludur")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; } = null!;

        [Display(Name ="Cep No")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [RegularExpression(@"^[0-9]{11}", ErrorMessage = "Telefon numaranızı başında 0 olarak, 11 haneli yazınız")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;


        //Navigation Properties
        [ValidateNever]
        public AppUserProfileViewModel? AppUserProfile { get; set; }
    }
}
