using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAndPurchaseManagement.Data;
using System.Linq;

namespace SalesAndPurchaseManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly SAPManagementContext _context;

        public DashboardController(SAPManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy danh sách nhân viên mới nhất, sắp xếp giảm dần theo EmployeeId.
            var latestEmployees = _context.Employees
                .OrderByDescending(e => e.EmployeeId)
                .Take(8) // Lấy tối đa 8 nhân viên
                .ToList();

            return View(latestEmployees); // Truyền danh sách nhân viên vào View
        }

    }
}
