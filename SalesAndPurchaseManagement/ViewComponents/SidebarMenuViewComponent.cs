using Microsoft.AspNetCore.Mvc;
using SalesAndPurchaseManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.ViewComponents
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();

        public SidebarMenuViewComponent()
        {
            MenuItems = new List<MenuItem>()
            {
                new MenuItem() { Id = 1, Name = "Tổng quan", Link = "/Dashboard/Index", Icon = "fas fa-tachometer-alt" },
                new MenuItem() { Id = 2, Name = "Nhân viên", Link = "/Employee/Index", Icon = "fas fa-user" },
                new MenuItem() { Id = 3, Name = "Vị trí công việc", Link = "/Job/Index", Icon = "fa fa-briefcase" },
                new MenuItem() { Id = 4, Name = "Sản phẩm", Link = "/Product/Index", Icon = "fa fa-user" },
                new MenuItem() { Id = 5, Name = "Đăng xuất", Link = "/Home/Logout", Icon = "fa fa-user" },


            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SidebarMenu", MenuItems);
        }
    }
}
