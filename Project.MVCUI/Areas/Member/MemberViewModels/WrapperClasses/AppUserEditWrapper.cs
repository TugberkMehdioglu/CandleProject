using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.Areas.Member.MemberViewModels.WrapperClasses
{
    public class AppUserEditWrapper
    {
        public AppUserViewModel? AppUser { get; set; }

        public PasswordChangeViewModel? PasswordChange { get; set; }

    }
}
