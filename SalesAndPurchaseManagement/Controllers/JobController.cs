using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace SalesAndPurchaseManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class JobController : Controller
    {
        private readonly SAPManagementContext _context;

        public JobController(SAPManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jobs = _context.Jobs.ToList();
            return View(jobs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Jobs.Add(job);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        public IActionResult Edit(int id)
        {
            var job = _context.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Job job)
        {
            if (id != job.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Jobs.Any(e => e.JobId == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        public IActionResult Delete(int id)
        {
            var job = _context.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var job = _context.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
