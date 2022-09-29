using DeviceManagement_WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using DeviceManagement_WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Components.Routing;

namespace DeviceManagement_WebApp.Repository
{
    public class ZonesRepository : GenericRepository<Zone>, IZonesRepository
    {

        public ZonesRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Zone GetMostRecentService()
        {
            return _context.Zone.OrderByDescending(zone => zone.DateCreated).FirstOrDefault();
        }

    }
}
