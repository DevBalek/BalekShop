using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
                
    }
}
