using System;
using System.ComponentModel.DataAnnotations;

namespace ParmisAuth.Data
{
	public class CartModel
	{
		[Required]
		public int Id { get; set; }
        public int User { get; set; }

		public int ProductList { get; set; }
	}
}

