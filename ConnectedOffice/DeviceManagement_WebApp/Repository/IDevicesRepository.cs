using DeviceManagement_WebApp.Models;
using System.Collections.Generic;

namespace DeviceManagement_WebApp.Repository
{
    public interface IDevicesRepository : IGenericRepository<Device>
    {
        Device GetMostRecentService();
        IEnumerable<Category> GetCategory();
        IEnumerable<Zone> GetZone();

    }
}
 