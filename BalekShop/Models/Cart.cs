using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalekShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserID { get; set; }

        [Required]
        public int BookID { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
