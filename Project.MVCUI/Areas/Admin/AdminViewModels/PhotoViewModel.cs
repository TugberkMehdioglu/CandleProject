using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class PhotoViewModel
    {
        public int? Id { get; set; }
        public string ImagePath { get; set; } = null!;
        public int ProductId { get; set; }


        //Navigation Properties
        [ValidateNever]
        public ProductViewModel? Product { get; set; }
    }
}
