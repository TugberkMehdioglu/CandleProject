using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Project.MVCUI.Areas.Member.MemberViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Adres Adı")]
        [Required(ErrorMessage ="{0} boş bırakılamaz")]
        [StringLength(35, MinimumLength = 2, ErrorMessage ="{0}, {2} ile {1} karakter arasında olmalıdır")]
        public string Name { get; set; } = null!;

        [Display(Name = "Ülke")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string Country { get; set; } = null!;

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string City { get; set; } = null!;

        [Display(Name = "Semt")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string District { get; set; } = null!;

        [Display(Name = "Mahalle")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string Neighborhood { get; set; } = null!;

        [Display(Name = "Sokak")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string Street { get; set; } = null!;

        [Display(Name = "Apartman No")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [DataType(DataType.Text)]
        [Range(1, 500, ErrorMessage = "{0} {1} ile {2} arasında olmalıdır")]
        [RegularExpression(@"^\d+", ErrorMessage = "Sadece rakam giriniz")]
        public ushort AptNo { get; set; }

        [Display(Name = "Daire")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^\d+", ErrorMessage = "Sadece rakam giriniz")]
        public byte? Flat { get; set; }//For private property

        [ValidateNever]
        public string FullAddress
        {
            get
            {
                return $"{Neighborhood} Mah. {Street} Sok. No:{AptNo} {(Flat.HasValue ? $"Daire:{Flat}" : "")}\n\n {City.ToUpper()} / {Country.ToUpper()}";
            }
        } //Will not be in database.

        [ValidateNever]
        public string AppUserProfileID { get; set; } = null!;
    }
}
