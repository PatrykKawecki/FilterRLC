using Charts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FilterRLC.Models
{
    public class FilterDbContext : DbContext
    {
        public DbSet<FilterModel> SetOfFilters { get; set; }
    }
}