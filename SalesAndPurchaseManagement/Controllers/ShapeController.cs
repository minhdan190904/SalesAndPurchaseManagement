using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Helpers;
using SalesAndPurchaseManagement.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    [Authorize]
    public class ShapeController : Controller
    {
        private readonly SAPManagementContext _context;

        public ShapeController(SAPManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var shapes = await _context.Shapes.ToListAsync();
            return View(shapes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shape shape)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shape);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shape);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var shape = await _context.Shapes.FindAsync(id);
            if (shape == null)
            {
                return NotFound();
            }
            return View(shape);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shape shape)
        {
            if (id != shape.ShapeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shape);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Shapes.Any(s => s.ShapeId == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shape);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var shape = await _context.Shapes.FindAsync(id);
            if (shape == null)
            {
                return NotFound();
            }
            return View(shape);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shapeToDelete = await _context.Shapes.FindAsync(id);

            if (shapeToDelete == null)
            {
                return NotFound();
            }

            _context.Shapes.Remove(shapeToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}