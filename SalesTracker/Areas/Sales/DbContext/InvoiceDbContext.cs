using Microsoft.EntityFrameworkCore;
using SalesTracker.Areas.Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTracker.Areas.Sales.DbContext
{
    public class InvoiceDbContext
    {
        
        // DbSet properties for each entity
        public DbSet<Service> Services { get; set; }
    }
}