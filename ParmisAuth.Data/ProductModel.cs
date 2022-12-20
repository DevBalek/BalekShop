using System;
using System.ComponentModel.DataAnnotations;

namespace ParmisAuth.Data
{
	public class ProductModel
	{
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }        

        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int ProductCategory { get; set; }
	}
}

