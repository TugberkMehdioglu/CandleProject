using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Project.MVCUI.Areas.Member.MemberViewModels;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class OrderViewModel
    {
        public int? Id { get; set; }
        public string? AppUserProfileID { get; set; }
        public int? AddressID { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? CreatedDate { get; set; }

        //Navigation Properties

        [ValidateNever]
        public ICollection<OrderDetailViewModel>? OrderDetails { get; set; }

        [ValidateNever]
        public AppUserProfileViewModel? AppUserProfile { get; set; }
    }
}
