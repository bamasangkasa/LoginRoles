using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Company.Models;
namespace Company.DAL
{
    public class FactoryContextInitializer:DropCreateDatabaseIfModelChanges<FactoryContext>
    {
        protected override void Seed(FactoryContext context)
        {
            City C1 = new City() { Id = 1, Name = "Ramallah" };
            City C2 = new City() { Id = 2, Name = "Nablus" };
            context.Cities.Add(C1);
            context.Cities.Add(C2);


            Product p1 = new Product() { City = C1, CityId = C1.Id, ExpirationDate = DateTime.Now, ProductionDate = DateTime.Now, Id = 1, Description = "", Photo = "6.jpg", Price = 55, Title = "Item 1" };
            Product p2 = new Product() { City = C2, CityId = C2.Id, ExpirationDate = DateTime.Now, ProductionDate = DateTime.Now, Id = 2, Description = "", Photo = "3.jpg", Price = 55, Title = "Item 2" };
            Product p3 = new Product() { City = C2, CityId = C2.Id, ExpirationDate = DateTime.Now, ProductionDate = DateTime.Now, Id = 3, Description = "", Photo = "6.jpg", Price = 51, Title = "Item 3" };
            Product p4 = new Product() { City = C1, CityId = C1.Id, ExpirationDate = DateTime.Now, ProductionDate = DateTime.Now, Id = 4, Description = "", Photo = "3.jpg", Price = 55, Title = "Item 4" };


            context.Products.Add(p1);
            context.Products.Add(p2);
            context.Products.Add(p3);
            context.Products.Add(p4);
           

            context.SaveChanges();
        }

    }
}