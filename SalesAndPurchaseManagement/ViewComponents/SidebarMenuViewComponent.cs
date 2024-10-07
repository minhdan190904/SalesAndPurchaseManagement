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
                new MenuItem() { Id = 1, Name = "Tổng quan", Link = "~/Dashboard/Index", Icon = "fas fa-tachometer-alt" },
                new MenuItem() { Id = 2, Name = "Nhân viên", Link = "~/Employee/Index", Icon = "fas fa-th" }
            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SidebarMenu", MenuItems);
        }
    }
}
