﻿namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class AssignRoleToUserViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool Exist { get; set; } //İlgili kullanıcıda bu rol var mı
    }
}
