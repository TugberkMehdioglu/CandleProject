using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.Areas.Member.MemberViewModels
{
    public class AppUserProfileViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "İsim")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Soyisim")]
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
        public AppUserViewModel? AppUser { get; set; }
    }
}
