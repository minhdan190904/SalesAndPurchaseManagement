using Microsoft.AspNetCore.Mvc;
using SalesAndPurchaseManagement.Models;

namespace SalesAndPurchaseManagement.ViewComponents
{
    public class RenderViewComponent:ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();
        public RenderViewComponent()
        {
            MenuItems = new List<MenuItem>() {
                new MenuItem() {Id=2, Name="Nhân viên", Link="Employee" },
                new MenuItem() {Id=1, Name="Công việc", Link="Job" },
            };
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu",MenuItems);
        }        
        
    }

}
