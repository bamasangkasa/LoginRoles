using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Company.Models;
namespace Company.DAL
{
    public class FactoryContext:DbContext
    {

        public FactoryContext():base("DefaultConnection"){}

        public DbSet<City> Cities { set; get; }
        public DbSet<Product> Products { set; get; }


    }
}