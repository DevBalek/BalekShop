using System.ComponentModel.DataAnnotations;

namespace BalekShop.Data;

public class UserModel
{

    [Required]	
	public int Id { get; set; }

	[Required]
    public string Username{ get; set; }

    [Required]	
	public string Email { get; set; }

    [Required]
	public string Password { get; set; }

	public string Adress { get; set; }

    public int UserCart { get; set; }

}


