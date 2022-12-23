using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalekShop.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]        
        public string UserName{ get; set; }
        [Required]
        public string Password { get; set; }
        [Required]        
        public string Email { get; set; }
        [Required]
        public string Adress { get; set; }

        [Required]
        public int CartId { get; set; }

        [NotMapped]
        public Cart? Cart { get; set; }
    }
}
