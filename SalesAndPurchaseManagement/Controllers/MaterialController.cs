using Microsoft.AspNetCore.Mvc;

namespace SalesAndPurchaseManagement.Controllers
{
    public class MaterialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
