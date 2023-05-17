using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationsInNetCore.Models
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext>options):base(options)
        {


        }
      public  DbSet<Brand> Brands{ get; set; }
    }
}
