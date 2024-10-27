using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace SalesAndPurchaseManagement.Controllers
{
    [Authorize]
    public class ColorController : Controller
    {
        private readonly SAPManagementContext _context;

        public ColorController(SAPManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var colors = _context.Colors.ToList();
            return View(colors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Color color)
        {
            if (ModelState.IsValid)
            {
                _context.Colors.Add(color);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        public IActionResult Edit(int id)
        {
            var color = _context.Colors.Find(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Color color)
        {
            if (id != color.ColorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(color);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Colors.Any(e => e.ColorId == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        public IActionResult Delete(int id)
        {
            var color = _context.Colors.Find(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var color = _context.Colors.Find(id);
            if (color == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(color);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}