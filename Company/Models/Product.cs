using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Models
{
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
	
}