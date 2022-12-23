using BalekShop.Models.Domain;
using BalekShop.Repositories.Abstract;

namespace BalekShop.Repositories.Implementation
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext context;
        public UserService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(User model)
        {
            try
            {
                context.User.Add(model);
                context.SaveChanges();
                User lastUser = GetAll().Last<User>();
                context.Cart.Add(new Cart { UserID = lastUser.Id });
                model.CartId = lastUser.Id;
                Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.User.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User FindById(int id)
        {
            return context.User.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            var data = (from user in context.User
                        join cart in context.Cart
                        on user.CartId equals cart.UserID
                        select new User
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Cart = cart,
                            Adress = user.Adress,
                            Email = user.Email,
                            Password = user.Password,                            
                        }
                        ).ToList();
            return data;
        }

        public bool Update(User model)
        {
            try
            {
                context.User.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
