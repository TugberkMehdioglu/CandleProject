using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public short Quentity { get; set; }
        public decimal SubTotal { get; set; }

        //Navigation Properties

        [ValidateNever]
        public OrderViewModel? Order { get; set; }

        [ValidateNever]
        public ProductViewModel? Product { get; set; }
    }
}
