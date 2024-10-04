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
                new MenuItem() {Id=2, Name="Employees", Link="Employee" },
                new MenuItem() {Id=1, Name="Jobs", Link="Job" },
            };
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu",MenuItems);
        }        
        
    }

}
