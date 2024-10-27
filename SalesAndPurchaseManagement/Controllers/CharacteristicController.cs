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
    public class CharacteristicController : Controller
    {
        private readonly SAPManagementContext _context;

        public CharacteristicController(SAPManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var characteristics = _context.Characteristics.ToList(); 
            return View(characteristics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Characteristic characteristic)
        {
            if (ModelState.IsValid)
            {
                _context.Characteristics.Add(characteristic); // Đã sửa ở đây
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(characteristic);
        }

        public IActionResult Edit(int id)
        {
            var characteristic = _context.Characteristics.Find(id); // Đã sửa ở đây
            if (characteristic == null)
            {
                return NotFound();
            }
            return View(characteristic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Characteristic characteristic)
        {
            if (id != characteristic.CharacteristicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characteristic);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Characteristics.Any(e => e.CharacteristicId == id)) // Đã sửa ở đây
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(characteristic);
        }

        public IActionResult Delete(int id)
        {
            var characteristic = _context.Characteristics.Find(id); // Đã sửa ở đây
            if (characteristic == null)
            {
                return NotFound();
            }
            return View(characteristic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var characteristic = _context.Characteristics.Find(id); // Đã sửa ở đây
            if (characteristic == null)
            {
                return NotFound();
            }

            _context.Characteristics.Remove(characteristic); // Đã sửa ở đây
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
