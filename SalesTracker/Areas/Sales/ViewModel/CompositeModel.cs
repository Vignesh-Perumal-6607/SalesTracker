using SalesTracker.Areas.Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTracker.Areas.Sales.ViewModel
{
    public class CompositeModel
    {
        public IEnumerable<Service> Services { get; set; }
       
    }
}