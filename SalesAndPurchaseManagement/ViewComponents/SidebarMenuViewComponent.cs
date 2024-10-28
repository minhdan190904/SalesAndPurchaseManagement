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
                    new MenuItem() { Id = 17, Name = "Chi tiết sản phẩm", Link = "/Product/Index", Icon = "fas fa-box" },
                    new MenuItem() { Id = 5, Name = "Danh mục", Link = "/Category/Index", Icon = "fas fa-list" },
                    new MenuItem() { Id = 6, Name = "Xuất xứ", Link = "/CountryOfOrigin/Index", Icon = "fas fa-globe" },
                    new MenuItem() { Id = 7, Name = "Đặc điểm", Link = "/Characteristic/Index", Icon = "fas fa-cogs" },
                    new MenuItem() { Id = 8, Name = "Màu sắc", Link = "/Color/Index", Icon = "fas fa-palette" },
                    new MenuItem() { Id = 8, Name = "Vật liệu", Link = "/Material/Index", Icon = "fas fa-cube" },
                    new MenuItem() { Id = 10, Name = "Tính năng", Link = "/Feature/Index", Icon = "fas fa-tools" },
                    new MenuItem() { Id = 11, Name = "Kích thước", Link = "/Size/Index", Icon = "fas fa-ruler" },
                    new MenuItem() { Id = 12, Name = "Hình dạng", Link = "/Shape/Index", Icon = "fas fa-shapes" },
                    new MenuItem() { Id = 13, Name = "Nhà sản xuất", Link = "/Manufacturer/Index", Icon = "fas fa-industry" }
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
