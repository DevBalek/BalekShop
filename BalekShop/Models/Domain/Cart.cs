using System.ComponentModel.DataAnnotations.Schema;

namespace BalekShop.Models.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserID { get; set; }

        [NotMapped]
        public List<Book>? Books { get; set; }
    }
}
