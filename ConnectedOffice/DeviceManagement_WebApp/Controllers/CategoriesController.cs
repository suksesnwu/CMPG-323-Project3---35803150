using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        // retrieves all Zone entries
        public async Task<IActionResult> Index()
        {
            var categories = _categoriesRepository.GetAll();

            // await _categoriesRepository.SaveChanges();
            return View(categories);
        }

        // retrieve one Zone from the database based on the ID parsed through
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);
            await _categoriesRepository.SaveChanges();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // create a new Zone entry on database
        public IActionResult Create()
        {
            return View();
        }

        // create a new Zone entry on database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _categoriesRepository.Add(category);
            await _categoriesRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // update an existing Zone entry the database
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);
            await _categoriesRepository.SaveChanges();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // update an existing Zone entry on the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            try
            {
                _categoriesRepository.Update(category);
                await _categoriesRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // delete an existing Zone entry on the database
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);
            await _categoriesRepository.SaveChanges();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // confirm an existing Zone entry on the database is deleted
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = _categoriesRepository.GetById(id);
            _categoriesRepository.Remove(category);
            await _categoriesRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //  checks if a Zone exists (based on the ID parsed through) before editing or deleting an item
        private bool CategoryExists(Guid id)
        {
            return _categoriesRepository.Exists(id);
        }
    }
}
