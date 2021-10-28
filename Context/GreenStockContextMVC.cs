
using GreenMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMVC.Context
{
    public class GreenStockContextMVC : DbContext
    {
       
            public GreenStockContextMVC(DbContextOptions<GreenStockContextMVC> options) : base(options)
            {

            }

            public DbSet<Product> Product { get; set; }
            public DbSet<Category> Category { get; set; }
        }


  
}
