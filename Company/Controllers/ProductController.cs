using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Company.Models;
using Company.DAL;
using System.IO;
using PagedList;
namespace Company.Controllers
{
    public class ProductController : Controller
    {
        private FactoryContext db = new FactoryContext();

        //
        // GET: /Product/

        public ViewResult Index(string sortOrder, string currentFilter,  int? page)
        {

       var searchString=     Request.Params.Get("SearchString");

            var products = db.Products.Include(p => p.City);
            var prs = products.ToList().Select(x=>x);
   
            /*
            //Its just for search based on title
            if (!String.IsNullOrEmpty(searchString))
            {
                prs = prs.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper())).Select(x=>x);
            }
            */
           



     

            ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (String.IsNullOrEmpty(sortOrder))
            {
                ViewBag.NameSortParm = "name_desc";
            }
            else
            {
                ViewBag.NameSortParm = "";
            }


            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

             prs = from s in products.ToList()
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                prs = prs.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    prs = prs.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    prs = prs.OrderBy(s => s.ProductionDate);
                    break;
                case "date_desc":
                    prs = prs.OrderByDescending(s => s.ProductionDate);
                    break;
                default:  // Name ascending 
                    prs = prs.OrderBy(s => s.Title);
                    break;
            }
       
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(prs.ToPagedList(pageNumber, pageSize));
           // return View(products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create
        [Authorize(Roles = "admin,developer")] 
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            return View();
        }

        //
        // POST: /Product/Create
        [Authorize(Roles = "admin,developer")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {


            if (ModelState.IsValid)
            {


                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(file.FileName);
                    string path2 = Path.GetRandomFileName();
                    fileName = path2 + fileName;
                    var path = Path.Combine(Server.MapPath("~/Upload/"), fileName);

                    product.Photo = fileName;

                    file.SaveAs(path);//saved the file
                }





                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", product.CityId);
            return View(product);
        }

        //
        // GET: /Product/Edit/5
         [Authorize(Roles = "admin,developer")] 
        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", product.CityId);
            return View(product);
        }

        //
        // POST: /Product/Edit/5
         [Authorize(Roles = "admin,developer")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", product.CityId);
            return View(product);
        }

        //
        // GET: /Product/Delete/5
         [Authorize(Roles = "admin")] 
        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5
         [Authorize(Roles = "admin")] 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult DisplayByCityName(string City) 
        {
            var AllProducts = db.Products.ToList();
            var ProCity = AllProducts.Where(x => x.City.Name == City).Select(p => p);

            return View("Index", ProCity.ToList());
        }

    }
}