using System.ComponentModel.DataAnnotations;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class CategoryViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "{0} zorunludur")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "{0}, {2} ile {1} karakter arasında olmalıdır")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
