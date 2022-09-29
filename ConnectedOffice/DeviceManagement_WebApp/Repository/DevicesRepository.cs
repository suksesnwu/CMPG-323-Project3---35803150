using DeviceManagement_WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using DeviceManagement_WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeviceManagement_WebApp.Repository
{
    public class DevicesRepository : GenericRepository<Device>, IDevicesRepository
    {
        public DevicesRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Device GetMostRecentService()
        {
            return _context.Device.OrderByDescending(device => device.DateCreated).FirstOrDefault();
        }

        public override IEnumerable<Device> GetAll()
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return connectedOfficeContext.ToList();
        }

        public override Device GetById(Guid? id)
        {
            return _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefault(m => m.DeviceId == id);
        }

        public IEnumerable<Category> GetCategory()
        {
            return _context.Category;
        }

        public IEnumerable<Zone> GetZone()
        {
            return _context.Zone;
        }
    }
}
