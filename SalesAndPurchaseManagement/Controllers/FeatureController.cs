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
    public class FeatureController : Controller
    {
        private readonly SAPManagementContext _context;

        public FeatureController(SAPManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var features = _context.Features.ToList();
            return View(features);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feature feature)
        {
            if (ModelState.IsValid)
            {
                _context.Features.Add(feature);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        public IActionResult Edit(int id)
        {
            var feature = _context.Features.Find(id);
            if (feature == null)
            {
                return NotFound();
            }
            return View(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Feature feature)
        {
            if (id != feature.FeatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feature);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Features.Any(e => e.FeatureId == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        public IActionResult Delete(int id)
        {
            var feature = _context.Features.Find(id);
            if (feature == null)
            {
                return NotFound();
            }
            return View(feature);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var feature = _context.Features.Find(id);
            if (feature == null)
            {
                return NotFound();
            }

            _context.Features.Remove(feature);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}