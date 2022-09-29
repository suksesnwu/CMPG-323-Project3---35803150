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
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
 
        public CategoriesRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Category GetMostRecentService()
        {
            return _context.Category.OrderByDescending(category => category.DateCreated).FirstOrDefault();
        }
    }
}
