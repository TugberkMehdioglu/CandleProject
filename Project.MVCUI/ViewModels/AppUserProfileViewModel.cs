using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Project.MVCUI.Areas.Member.MemberViewModels;
using System.ComponentModel.DataAnnotations;

namespace Project.MVCUI.ViewModels
{
    public class AppUserProfileViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "İsim")]
        [Required(ErrorMessage ="{0} zorunludur")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        public string FirstName { get; set; } = null!;

        [Display(Name ="Soyisim")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        public string LastName { get; set; } = null!;

        [ValidateNever]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [ValidateNever]
        public string? ImagePath { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }


        //Navigation Properties
        [ValidateNever]
        public ICollection<AddressViewModel>? Addresses { get; set; }
    }
}
