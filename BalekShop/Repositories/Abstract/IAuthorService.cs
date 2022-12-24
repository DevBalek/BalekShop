using BalekShop.Models;

namespace BalekShop.Repositories.Abstract
{
    public interface IAuthorService
    {
        bool Add(Author model);
        bool Update(Author model);
        bool Delete(int id);
        Author FindById(int id);
        IEnumerable<Author> Get();
    }
}
