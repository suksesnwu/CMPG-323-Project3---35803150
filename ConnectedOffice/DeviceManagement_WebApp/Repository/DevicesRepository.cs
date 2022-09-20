using DeviceManagement_WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using DeviceManagement_WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement_WebApp.Repository
{
    public class DevicesRepository
    {
        private readonly ConnectedOfficeContext _context;

        // GET: Devices
        public List<Device> GetAll()
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return connectedOfficeContext.ToList();
        }
    }
}
