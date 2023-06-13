using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;

        public BloodDonorController(BloodDonorDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.BloodDonors.ToList();
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _context.BloodDonors.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BloodDonor user)
        {
            if (ModelState.IsValid)
            {
                _context.BloodDonors.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _context.BloodDonors.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(BloodDonor user)
        {
            if (ModelState.IsValid)
            {
                _context.BloodDonors.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _context.BloodDonors.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.BloodDonors.FirstOrDefault(u => u.Id == id);
            _context.BloodDonors.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
