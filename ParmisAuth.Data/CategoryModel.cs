using System;
using System.ComponentModel.DataAnnotations;

namespace ParmisAuth.Data
{
	public class CategoryModel
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string CategoryName { get; set; }
	}
}

