using BalekShop.Models;

namespace BalekShop.Repositories.Abstract
{
    public interface IAdminService
	{
		bool Add(Admin model);
		bool Update(Admin model);
		bool Delete(int id);
		Admin FindById(int id);
		IEnumerable<Admin> Get();
	}
}
