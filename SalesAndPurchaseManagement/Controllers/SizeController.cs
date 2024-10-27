using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Helpers;
using SalesAndPurchaseManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    public class SizeController : Controller
    {
        private readonly SAPManagementContext _context;

        public SizeController(SAPManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sizes = await _context.Sizes.ToListAsync();
            return View(sizes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Size size)
        {
            if (ModelState.IsValid)
            {
                _context.Add(size);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Size size)
        {
            if (id != size.SizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(size);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Sizes.Any(s => s.SizeId == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sizeToDelete = await _context.Sizes.FindAsync(id);

            if (sizeToDelete == null)
            {
                return NotFound();
            }

            _context.Sizes.Remove(sizeToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}