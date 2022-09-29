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
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class ZonesController : Controller
    {
        private readonly IZonesRepository _zonesRepository;
        public ZonesController(IZonesRepository zonesRepository)
        {
            _zonesRepository = zonesRepository;
        }

        //  retrieves all Zone entries from the database
        public async Task<IActionResult> Index()
        {
            var zones = _zonesRepository.GetAll();
            await _zonesRepository.SaveChanges();
            return View(zones);
        }

        // retrieve one Zone from the database based on the ID parsed through
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zonesRepository.GetById(id);
            await _zonesRepository.SaveChanges();
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // create a new Zone entry on the database
        public IActionResult Create()
        {
            return View();
        }

        // create a new Zone entry on database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {

            zone.ZoneId = Guid.NewGuid();
            _zonesRepository.Add(zone);
            await _zonesRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // update an existing Zone entry on the database
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zonesRepository.GetById(id);
            await _zonesRepository.SaveChanges();
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // update an existing Zone entry on the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            try
            {
                _zonesRepository.Update(zone);
                await _zonesRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
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

        // delete an existing Zone entry the database
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zonesRepository.GetById(id);
            await _zonesRepository.SaveChanges();

            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // confirm an existing Zone entry on the database is deleted

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = _zonesRepository.GetById(id);
            _zonesRepository.Remove(zone);
            await _zonesRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // checks if a Zone exists (based on the ID parsed through) before editing or deleting an item
        private bool ZoneExists(Guid id)
        {
            return _zonesRepository.Exists(id);
        }
    }
}
