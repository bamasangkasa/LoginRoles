 /*
 Create Company which has factory
 -product
 -city
 
 create pagination for product (page size is 3) ordering by name,date Asc , Desc
 
 
 create three roles and one superadmin 
 -roles : admin , developer , user
 -users : 
  superadmin password1234   admin
  user1      password1234   developer
  user2		 password1234   user
  
  City { only super can manipulate cities )  [Authorize(Users="superadmin")]
  Product { Only admin can delete/edit/create product ) [Authorize(Role="admin")] 
		  { Developer Can Create/Edit product ) [Authorize(Role="developer")]
  
  
           @if (User.IsInRole("admin") ||User.IsInRole("developer") )
         {
         <h1>Hi Admin</h1>
         
         }

         @if (User.IsInRole("developer"))
         {
         <h1>Hi Admin</h1>
         
         }
  
  
  
  
  
  
  
  
  create route forthe products 
  all/{City} ::: all/ramallah to display all the products inRamallah ...
  
  the default landing page should be the all the product listing
  
  create context initializer


*/

 public class City
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }
	
	
	
	
	
	
	
	
	    public class Product
    {

        public int Id { set; get; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Title { set; get; }




        [Display(Name = "Production Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ProductionDate { get; set; }

        [DisplayName("Expiration Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ExpirationDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { set; get; }


        [Required(ErrorMessage = "Photo is required")]
        public string Photo { set; get; }


     

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100.00,
            ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }

        public virtual City City { set; get; }
        public int CityId { set; get; }

    }
	
	
	
	
	
	
	    public class FactoryContext:DbContext
    {

        public DbSet<Product> Products { set; get; }
        public DbSet<City> Cities { set; get; }

        public FactoryContext() : base("DefaultConnection") { }



    }
	
	
	
	
	
	
	
	
	
	
	    public class FactoryContextInitializer:DropCreateDatabaseIfModelChanges<FactoryContext> //DropCreateDatabaseAlways Seed
    {
        protected override void Seed(FactoryContext context)
        {
            City C1=new City(){Id=1,Name="Ramallah"};
            City C2=new City(){Id=2,Name="Nablus"};
            context.Cities.Add(C1);
            context.Cities.Add(C2);


            Product p1 = new Product() { City=C1,CityId=C1.Id,ExpirationDate=DateTime.Now,ProductionDate=DateTime.Now,Id=1,Description="",Photo="6.jpg",Price=55,Title="Item 1"};
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
	
	
	
	
	
	
	
	Database.SetInitializer<FactoryContext>(new Company.DAL.FactoryContextInitializer());
	
	
	
	
	
	
	
	@model IEnumerable<Company.Models.Product>

@{
    ViewBag.Title = "Index";
}

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table>
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.Title)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.ProductionDate)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.ExpirationDate)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Description)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Photo)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Price)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.City.Name)
        </th>
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.Title)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.ProductionDate)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.ExpirationDate)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Description)
        </td>
        <td>
                  <img src="@Url.Content("~/Upload/"+ @Html.DisplayFor(modelItem => item.Photo))" width="600" height="400" style="
    width: 100px;
    height: 100px;
"/>
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Price)
        </td>
        <td>
            <a href="~/all/@Html.DisplayFor(modelItem => item.City.Name)">   @Html.DisplayFor(modelItem => item.City.Name)</a>
         
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", new { id=item.Id }) |
            @Html.ActionLink("Details", "Details", new { id=item.Id }) |
            @Html.ActionLink("Delete", "Delete", new { id=item.Id })
        </td>
    </tr>
}

</table>




















@model Company.Models.Product

@{
    ViewBag.Title = "Details";
}

<h2>Details</h2>

<fieldset>
    <legend>Product</legend>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.Title)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.Title)
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.ProductionDate)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.ProductionDate)
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.ExpirationDate)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.ExpirationDate)
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.Description)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.Description)
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.Photo)
    </div>
    <div class="display-field">
     
    <img src="@Url.Content("~/Upload/"+ @Html.DisplayFor(model=> model.Photo))" width="600" height="400" />
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.Price)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.Price)
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.City.Name)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.City.Name)
    </div>
</fieldset>
<p>
    @Html.ActionLink("Edit", "Edit", new { id=Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>

	
	
	
	
	
	
	
	
	
	
	
	@model Company.Models.Product

@{
    ViewBag.Title = "Create";
}

<h2>Create</h2>

@using  (Html.BeginForm("Create", "Product", FormMethod.Post, new { enctype = "multipart/form-data" })) {
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(true)

    <fieldset>
        <legend>Product</legend>

        <div class="editor-label">
            @Html.LabelFor(model => model.Title)
        </div>
        <div class="editor-field">
            @Html.EditorFor(model => model.Title)
            @Html.ValidationMessageFor(model => model.Title)
        </div>

        <div class="editor-label">
            @Html.LabelFor(model => model.ProductionDate)
        </div>
        <div class="editor-field">
            @Html.EditorFor(model => model.ProductionDate)
            @Html.ValidationMessageFor(model => model.ProductionDate)
        </div>

        <div class="editor-label">
            @Html.LabelFor(model => model.ExpirationDate)
        </div>
        <div class="editor-field">
            @Html.EditorFor(model => model.ExpirationDate)
            @Html.ValidationMessageFor(model => model.ExpirationDate)
        </div>

        <div class="editor-label">
            @Html.LabelFor(model => model.Description)
        </div>
        <div class="editor-field">
            @Html.EditorFor(model => model.Description)
            @Html.ValidationMessageFor(model => model.Description)
        </div>

        <div class="editor-label">
            @Html.LabelFor(model => model.Photo)
        </div>
  

        <div class="editor-field">
        <input type="file" name="Photo" />
               @Html.ValidationMessageFor(model => model.Photo)
             </div>

        <div class="editor-label">
            @Html.LabelFor(model => model.Price)
        </div>
        <div class="editor-field">
            @Html.EditorFor(model => model.Price)
            @Html.ValidationMessageFor(model => model.Price)
        </div>

        <div class="editor-label">
            @Html.LabelFor(model => model.CityId, "City")
        </div>
        <div class="editor-field">
            @Html.DropDownList("CityId", String.Empty)
            @Html.ValidationMessageFor(model => model.CityId)
        </div>

        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}










	
	
	
	
	
	
	
	
	
	
	
	
	        public ActionResult DisplayByCityName(string City)
        {
            var AllProducts = db.Products.ToList();
            var ProCity = AllProducts.Where(x => x.City.Name == City).Select(p => p);

            return View("Index", ProCity.ToList());
        }
		
		
		
		
		
		
		
		
		
		
		
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
		
		
		
		
		
		
		
		         @if (User.IsInRole("admin"))
         {
         <h1>Hi Admin</h1>
         
         }

         @if (User.IsInRole("developer"))
         {
         <h1>Hi Admin</h1>
         
         }
		
		
		
	
	
	
