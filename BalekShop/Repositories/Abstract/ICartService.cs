using BalekShop.Models;

namespace BalekShop.Repositories.Abstract
{
    public interface ICartService
    {
        bool Add(Cart model);
        bool Update(Cart model);
        bool Delete(int id);
        Cart FindById(int id);
        public List<Cart> FindByCartsUserId(int id);

		IEnumerable<Cart> Get();
    }
}
