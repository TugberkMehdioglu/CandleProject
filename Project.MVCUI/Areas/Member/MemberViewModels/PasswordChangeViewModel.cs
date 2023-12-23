using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.Areas.Member.MemberViewModels
{
    public class PasswordChangeViewModel
    {
        [Display(Name = "Mevcut Şifre")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

        [Display(Name = "Yeni Şifre Tekrar")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler uyuşmuyor")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirm { get; set; } = null!;
    }
}
