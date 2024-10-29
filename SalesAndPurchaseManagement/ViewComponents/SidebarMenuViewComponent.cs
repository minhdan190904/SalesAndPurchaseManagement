using Microsoft.AspNetCore.Mvc;
using SalesAndPurchaseManagement.Models;

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
            new MenuItem()
            {
                Id = 4,
                Name = "Sản phẩm",
                Link = "#",
                Icon = "fa fa-box",
                SubItems = new List<MenuItem>
                {
                    new MenuItem() { Id = 17, Name = "Chi tiết sản phẩm", Link = "/Product/Index" },
                    new MenuItem() { Id = 5, Name = "Danh mục", Link = "/Category/Index" },
                    new MenuItem() { Id = 6, Name = "Xuất xứ", Link = "/CountryOfOrigin/Index" },
                    new MenuItem() { Id = 7, Name = "Đặc điểm", Link = "/Characteristic/Index" },
                    new MenuItem() { Id = 9, Name = "Vật liệu", Link = "/Material/Index" },
                    new MenuItem() { Id = 10, Name = "Tính năng", Link = "/Feature/Index" },
                    new MenuItem() { Id = 12, Name = "Hình dạng", Link = "/Shape/Index" },
                    new MenuItem() { Id = 13, Name = "Nhà sản xuất", Link = "/Manufacturer/Index" }
                }
            },
            new MenuItem() { Id = 14, Name = "Khách hàng", Link = "/Customer/Index", Icon = "fas fa-users" },
            new MenuItem() { Id = 15, Name = "Nhà cung cấp", Link = "/Supplier/Index", Icon = "fas fa-truck" },
            new MenuItem() { Id = 16, Name = "Đăng xuất", Link = "/Home/Logout", Icon = "fa fa-sign-out-alt" }
        };
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("SidebarMenu", MenuItems);
    }
}
