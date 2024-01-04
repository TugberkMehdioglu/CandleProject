using Project.MVCUI.Areas.Admin.AdminViewModels;

namespace Project.MVCUI.ViewModels.WrapperClasses
{
    public class ShoppingWrapper
    {
        public ICollection<ProductViewModel>? Products { get; set; }
        public ICollection<CategoryViewModel>? Categories { get; set; }
    }
}
