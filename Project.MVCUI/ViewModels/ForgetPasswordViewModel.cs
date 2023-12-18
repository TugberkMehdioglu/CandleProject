using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [EmailAddress(ErrorMessage = "Doğru formatta {0} giriniz")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "{0}, {2} ile {1} karakter arasında olabilir")]
        [DataType(DataType.Text)]
        public string Email { get; set; } = null!;

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Şifre Onay")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; } = null!;
    }
}
