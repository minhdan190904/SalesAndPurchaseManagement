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
    public class ManufacturerController : Controller
    {
        private readonly SAPManagementContext _context;

        public ManufacturerController(SAPManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var manufacturers = _context.Manufacturers.ToList();
            return View(manufacturers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                _context.Manufacturers.Add(manufacturer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        public IActionResult Edit(int id)
        {
            var manufacturer = _context.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return View(manufacturer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Manufacturer manufacturer)
        {
            if (id != manufacturer.ManufacturerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manufacturer);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Manufacturers.Any(e => e.ManufacturerId == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        public IActionResult Delete(int id)
        {
            var manufacturer = _context.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return View(manufacturer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var manufacturer = _context.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            _context.Manufacturers.Remove(manufacturer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}